﻿using CyberSource.Api;
using CyberSource.Model;
using System;

namespace Cybersource_rest_samples_dotnet.Samples.Payments.ElectronicCheck
{
    /**
     * This is the sample code to do Credit transaction to bank account
	 */
    class ProcessEcheckCredit
    {
        public static PtsV2CreditsPost201Response Run()
        {
            var requestObj = new CreateCreditRequest();

            var v2PaymentsClientReferenceInformationObj = new Ptsv2paymentsClientReferenceInformation
            {
                Code = "test_credits"
            };
			
            requestObj.ClientReferenceInformation = v2PaymentsClientReferenceInformationObj;
			
            var v2PaymentsOrderInformationBillToObj = new Ptsv2paymentsidcapturesOrderInformationBillTo
            {
                Country = "US",
                FirstName = "John",
                LastName = "Doe",
                PhoneNumber = "4158880000",
                Address1 = "1 Market St",
                PostalCode = "94105",
                Locality = "san francisco",
                AdministrativeArea = "CA",
                Email = "test@cybs.com"
            };

            var v2PaymentsOrderInformationAmountDetailsObj = new Ptsv2paymentsidcapturesOrderInformationAmountDetails
            {
                TotalAmount = "200",
                Currency = "usd"
            };            

            var v2PaymentsOrderInformationObj = new Ptsv2paymentsidrefundsOrderInformation();
            v2PaymentsOrderInformationObj.BillTo = v2PaymentsOrderInformationBillToObj;
			v2PaymentsOrderInformationObj.AmountDetails = v2PaymentsOrderInformationAmountDetailsObj;

            requestObj.OrderInformation = v2PaymentsOrderInformationObj;
            
			var bankAccountObj = new Ptsv2paymentsPaymentInformationBankAccount
            {
                Number = "4100",
                Type = "C",
                CheckNumber = "123456"
            };

            var bankObj = new Ptsv2paymentsPaymentInformationBank
            {
                Account = bankAccountObj
            };

            bankObj.RoutingNumber = "071923284";

            var paymentInformationObj = new Ptsv2paymentsidrefundsPaymentInformation();
            paymentInformationObj.Bank = bankObj;
			
            requestObj.PaymentInformation = paymentInformationObj;

            try
            {
                var configDictionary = new Configuration().GetConfiguration();
                var clientConfig = new CyberSource.Client.Configuration(merchConfigDictObj: configDictionary);
                var apiInstance = new CreditApi(clientConfig);

                var result = apiInstance.CreateCredit(requestObj);
				
                Console.WriteLine(result);
				
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception on calling the API: " + e.Message);
                return null;
            }
        }
    }
}
