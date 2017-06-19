using System;
using Foundation;using StoreKit;namespace ProlificLibrary.iOS.Subscription
{
    public class IAPHelper : SKProductsRequestDelegate, ISKPaymentTransactionObserver
    {
        private SKProductsRequest productsRequest;
        private Action<SKProduct[]> completionBlock;
        private NSSet productIdentifiers;

        public IAPHelper(NSSet identifiers)
        {
            productIdentifiers = identifiers;
            base.Init();

            SKPaymentQueue.DefaultQueue.AddTransactionObserver(this);
        }        public override void ReceivedResponse(SKProductsRequest request, SKProductsResponse response)        {            if ((response != null) && (response.Products != null))            {                completionBlock(response.Products);
                completionBlock = null;
                productsRequest = null;            }        }        public void RequestProducts(Action<SKProduct[]> completion)        {
            productsRequest?.Cancel();
            completionBlock = completion;
            productsRequest = new SKProductsRequest(productIdentifiers);
            productsRequest.Delegate = this;
            productsRequest.Start();        }

        public void BuyProduct(SKProduct product)
        {
            var payment = new SKMutablePayment();
            payment.ProductIdentifier = product.ProductIdentifier;
            SKPaymentQueue.DefaultQueue.AddPayment(payment);
        }        public void UpdatedTransactions(SKPaymentQueue queue, SKPaymentTransaction[] transactions)        {            foreach (var transaction in transactions) 
                switch (transaction.TransactionState) {
	                case SKPaymentTransactionState.Purchased:	                    completeTransaction(transaction);
	                    break;
	                case SKPaymentTransactionState.Failed:                        failedTransaction(transaction);	                    break;
	                case SKPaymentTransactionState.Restored:                        restoredTransaction(transaction);    
                        break;
	                default:
	                    break;
            }
        }        private void restoredTransaction(SKPaymentTransaction transaction)        {            throw new NotImplementedException();        }        private void failedTransaction(SKPaymentTransaction transaction)        {            Console.WriteLine($"{transaction?.Error?.Code}, {transaction?.Error.LocalizedDescription}");        }        private void completeTransaction(SKPaymentTransaction transaction)        {            deliverPurchaseNotificationForIdentifier(new NSString(transaction.Payment.ProductIdentifier));
            SKPaymentQueue.DefaultQueue.FinishTransaction(transaction);        }        private void deliverPurchaseNotificationForIdentifier(NSString productIdentifier)        {            if (productIdentifier != null) {
                NSNotificationCenter.DefaultCenter.PostNotificationName("Purchase complete", productIdentifier);
            }        }    }
}
