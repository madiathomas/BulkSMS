# BulkSMS
BulkSMS is an open source .NET Standard library for sending SMSes using Bulk SMS. It is very simple to use. It is using RestSharp to make HTTP calls to the Bulk SMS API in the background.

To send SMS, you first initialise a BulkSMSer object:

var bulkSMSer = new BulkSMSer("username", "password");

The add this command to send message:

bulkSMSer.SendSMS("Test message", "2701234567");

To retrieve profile information, you simply make this call:

bulkSMSer.GetProfile();

If you don't have an account with Bulk SMS yet, you can signup for it on Bulk SMS website -> https://www.bulksms.com/. They will give you some credits that you can use to test with.

This library will be extended in future to include other API calls currently not covered. These are the only API calls that I am making using Bulk SMS at the moment.
