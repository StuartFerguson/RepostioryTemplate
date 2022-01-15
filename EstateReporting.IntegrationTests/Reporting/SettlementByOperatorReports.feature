@base @shared
Feature: SettlementByOperatorReports

Background: 

	Given I create the following api scopes
	| Name                 | DisplayName                       | Description                            |
	| estateManagement     | Estate Managememt REST Scope      | A scope for Estate Managememt REST     |
	| transactionProcessor | Transaction Processor REST  Scope | A scope for Transaction Processor REST |

	Given the following api resources exist
	| ResourceName     | DisplayName            | Secret  | Scopes           | UserClaims                 |
	| estateManagement | Estate Managememt REST | Secret1 | estateManagement | MerchantId, EstateId, role |
	| transactionProcessor | Transaction Processor REST | Secret1 | transactionProcessor |  |

	Given the following clients exist
	| ClientId      | ClientName     | Secret  | AllowedScopes    | AllowedGrantTypes  |
	| serviceClient | Service Client | Secret1 | estateManagement,transactionProcessor | client_credentials |

	Given I have a token to access the estate management and transaction processor resources
	| ClientId      | 
	| serviceClient | 

	Given I have created the following estates
	| EstateName    |
	| Test Estate1 |

	Given I have created the following operators
	| EstateName   | OperatorName        | RequireCustomMerchantNumber | RequireCustomTerminalNumber |
	| Test Estate1 | Safaricom           | True                        | True                        |
	| Test Estate1 | Voucher | True                        | True                        |

	Given I create a contract with the following values
	| EstateName   | OperatorName        | ContractDescription          |
	| Test Estate1 | Safaricom           | Safaricom Contract           |
	| Test Estate1 | Voucher | HealthCare Centre 1 Contract |

	When I create the following Products
	| EstateName   | OperatorName | ContractDescription          | ProductName    | DisplayText | Value |
	| Test Estate1 | Safaricom    | Safaricom Contract           | Variable Topup | Custom      |       |
	| Test Estate1 | Voucher      | HealthCare Centre 1 Contract | 10 KES         | 10 KES      | 10      |

	When I add the following Transaction Fees
	| EstateName   | OperatorName | ContractDescription          | ProductName      | CalculationType | FeeDescription      | Value |
	| Test Estate1 | Safaricom    | Safaricom Contract           | Variable Topup   | Percentage      | Merchant Commission | 0.50  |
	| Test Estate1 | Voucher      | HealthCare Centre 1 Contract | 10 KES | Percentage      | Merchant Commission | 0.95  |

	Given I create the following merchants
	| MerchantName    | AddressLine1   | Town     | Region      | Country        | ContactName    | EmailAddress                 | EstateName    | SettlementSchedule |
	| Test Merchant 1 | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 1 | testcontact1@merchant1.co.uk | Test Estate1 | Weekly          |
	| Test Merchant 2 | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 2 | testcontact2@merchant2.co.uk | Test Estate1 | Monthly             |

	Given I have assigned the following  operator to the merchants
	| OperatorName | MerchantName    | MerchantNumber | TerminalNumber | EstateName    |
	| Safaricom    | Test Merchant 1 | 00000001       | 10000001       | Test Estate1 |
	| Voucher    | Test Merchant 2 | 00000002       | 10000002       | Test Estate1 |

	Given I have assigned the following devices to the merchants
	| DeviceIdentifier | MerchantName    | EstateName    |
	| 123456780        | Test Merchant 1 | Test Estate1 |
	| 123456781        | Test Merchant 2 | Test Estate1 |

	Given I make the following manual merchant deposits 
	| Reference | Amount   | DateTime | MerchantName    | EstateName    |
	| Deposit1  | 50000.00 | Today    | Test Merchant 1 | Test Estate1 |
	| Deposit1  | 50000.00 | Today    | Test Merchant 2 | Test Estate1 |

	When I perform the following transactions
	| DateTime   | TransactionNumber | TransactionType | MerchantName    | DeviceIdentifier | EstateName   | OperatorName | TransactionAmount | CustomerAccountNumber | CustomerEmailAddress | ContractDescription          | ProductName    | RecipientMobile |
	| 2022-01-06 | 1                 | Sale            | Test Merchant 1 | 123456780        | Test Estate1 | Safaricom    | 100.00            | 123456789             |                      | Safaricom Contract           | Variable Topup |                 |
	| 2022-01-06 | 2                 | Sale            | Test Merchant 1 | 123456780        | Test Estate1 | Safaricom    | 50.00             | 123456789             |                      | Safaricom Contract           | Variable Topup |                 |
	| 2022-01-06 | 3                 | Sale            | Test Merchant 1 | 123456780        | Test Estate1 | Safaricom    | 25.00             | 123456789             |                      | Safaricom Contract           | Variable Topup |                 |
		
	| 2022-01-06 | 1                 | Sale            | Test Merchant 2 | 123456781        | Test Estate1 | Voucher      | 10.00             | 123456789             |                      | HealthCare Centre 1 Contract | 10 KES         | 123456789       |
	| 2022-01-06 | 2                 | Sale            | Test Merchant 2 | 123456781        | Test Estate1 | Voucher      | 10.00             | 123456789             |                      | HealthCare Centre 1 Contract | 10 KES         | 123456789       |
	| 2022-01-06 | 3                 | Sale            | Test Merchant 2 | 123456781        | Test Estate1 | Voucher      | 10.00             | 123456789             |                      | HealthCare Centre 1 Contract | 10 KES         | 123456789       |
	
	Then transaction response should contain the following information
	| EstateName    | MerchantName    | TransactionNumber | ResponseCode | ResponseMessage |
	| Test Estate1 | Test Merchant 1 | 1                 | 0000         | SUCCESS         |
	| Test Estate1 | Test Merchant 1 | 2                 | 0000         | SUCCESS         |
	| Test Estate1 | Test Merchant 1 | 3                 | 0000         | SUCCESS         |
	
	| Test Estate1 | Test Merchant 2 | 1                 | 0000         | SUCCESS         |
	| Test Estate1 | Test Merchant 2 | 2                 | 0000         | SUCCESS         |
	| Test Estate1 | Test Merchant 2 | 3                 | 0000         | SUCCESS         |

	When I get the pending settlements the following information should be returned
	| SettlementDate | EstateName   | NumberOfFees |
	| 2022-01-13     | Test Estate1 | 3            |
	| 2022-02-06     | Test Estate1 | 3            |

	When I process the settlement for '2022-01-13' on Estate 'Test Estate1' then 3 fees are marked as settled and the settlement is completed
	When I process the settlement for '2022-02-06' on Estate 'Test Estate1' then 3 fees are marked as settled and the settlement is completed

Scenario: Settlement By Operator
	When I get the Estate Settlement By Operator Report for Estate 'Test Estate1' with the Start Date '2022-01-13' and the End Date '2022-02-06' the following data is returned
	| Operator            | NumberOfFeesSettled | ValueOfFeesSettled |
	| Safaricom           | 3                   | 0.88               |
	| HealthCare Centre 1 | 3                   | 0.30               |