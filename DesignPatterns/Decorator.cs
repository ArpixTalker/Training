using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    public class PurchaseOrder //Object to be decorated
    {
        public string PurchaseID { get; set; }
        public string ItemNumber { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        public double PriceUnit { get; set; }
        public double PriceTotal { get; set; }
        public string ReceivedAt { get; set; }
        public string PricedAt { get; set; }
        public string ShippedAt { get; set; }
        public List<string> Log { get; }

        public PurchaseOrder(string itemNumber, int quantity) {

            this.Log = new List<string>();
            this.ItemNumber = itemNumber;
            this.Quantity = quantity;
        }

        public void AddLog(string text) {
            this.Log.Add(text);
        }

        public override string ToString()
        {
            return $"Purchase Order: {this.PurchaseID}, Received At: {this.ReceivedAt}" +
                $"\n Item: {this.ItemNumber}," +
                $"\n Qty: {this.Quantity}" +
                $"\n UnitPrice: {this.PriceUnit}" +
                $"\n Discount: {this.Discount}" +
                $"\n TotalPrice: {this.PriceTotal}" +
                $"\n Priced: {this.PricedAt}" +
                $"\n Shipped: {this.ShippedAt}";
        }

        public void PrintLog() {

            foreach (string line in this.Log) {
                Console.WriteLine($"Line: {line}");
            }
        }
    }

    public interface IPurchaseOrderProcessor { //Decorator interface

        PurchaseOrder ProcessPurchaseOrder();
    }
    
    public class PurchaseOrderProcessor : IPurchaseOrderProcessor{ //Base Decorator class

        protected PurchaseOrder po;

        public PurchaseOrderProcessor(PurchaseOrder po) {

            this.po = po;
        }

        public virtual PurchaseOrder ProcessPurchaseOrder() { //virtual method to be overidden in derived Decorators

            return po;
        }
    }

    public class PO_Receiver : PurchaseOrderProcessor //Receiver decorator
    {

        public PO_Receiver(PurchaseOrder po) : base(po) {

            this.po = po;
        }

        public override PurchaseOrder ProcessPurchaseOrder() { //Decorated method

            this.po.PurchaseID = DateTime.Now.Ticks.ToString();
            this.po.ReceivedAt = DateTime.UtcNow.ToString();
            this.po.AddLog($"Received at: {this.po.ReceivedAt}");
            return this.po;
        }
    }

    public class PO_PriceEvaluator : PurchaseOrderProcessor //Price Decorator
    {
        private Random rand = new Random();
        public PO_PriceEvaluator(PurchaseOrder po) : base(po){

            this.po = po;
        }

        public override PurchaseOrder ProcessPurchaseOrder() { //Decorated method

            this.po.PriceUnit = this.GetUnitPrice();
            this.po.Discount = this.getDiscount();
            this.po.PriceTotal = ((double)(this.po.PriceUnit * this.po.Quantity) * (1.0 - this.po.Discount));
            this.po.PricedAt = DateTime.UtcNow.ToString();
            this.po.AddLog($"Priced at: {DateTime.UtcNow.ToString()}");
            return this.po;
        }

        private double GetUnitPrice() { //Other private methods participating in decoration

            double unitPrice = rand.Next(0, 101) + 100;
            this.po.AddLog($"Unit Price obtained from DB: {unitPrice}");
            return unitPrice;
        }

        private double getDiscount() {

            double discount = (double)(rand.Next(0, 3) / 10);
            this.po.AddLog($"Discount obtained from DB: {discount}");
            return discount;
        }
    }

    public class PO_Shipment: PurchaseOrderProcessor //Shipment decorator
    {
        public PO_Shipment(PurchaseOrder po) : base(po) {
            this.po = po;
        }

        public override PurchaseOrder ProcessPurchaseOrder() //Decorated method
        {
            this.po.ShippedAt = DateTime.UtcNow.ToString();
            this.po.AddLog($"Shipped at: {this.po.ShippedAt}");
            return this.po;
        }
    }
    /*
     -- MAIN --

                Random rand = new Random();
                var po = new PurchaseOrder("item_12315", rand.Next(10,50));
                IPurchaseOrderProcessor receiver = new PO_Receiver(po);
                IPurchaseOrderProcessor pricer = new PO_PriceEvaluator(receiver.ProcessPurchaseOrder());
                IPurchaseOrderProcessor shipper = new PO_PriceEvaluator(pricer.ProcessPurchaseOrder());
                po = shipper.ProcessPurchaseOrder();
                Console.WriteLine(po.ToString());
         
                po.PrintLog();
                Console.Read();
                Console.Clear();
     
     */
}
