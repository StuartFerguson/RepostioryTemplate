@base @shared
Feature: TransactionsByMonthReports

Background: 

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
	| Test Estate 1 |
	| Test Estate 2 |

	Given I have created the following operators
	| EstateName    | OperatorName | RequireCustomMerchantNumber | RequireCustomTerminalNumber |
	| Test Estate 1 | Safaricom    | True                        | True                        |
	| Test Estate 2 | Safaricom    | True                        | True                        |

	Given I create a contract with the following values
	| EstateName    | OperatorName    | ContractDescription |
	| Test Estate 1 | Safaricom | Safaricom Contract |
	| Test Estate 2 | Safaricom | Safaricom Contract |

	When I create the following Products
	| EstateName    | OperatorName    | ContractDescription | ProductName    | DisplayText | Value  |
	| Test Estate 1 | Safaricom | Safaricom Contract | Variable Topup | Custom      |        |
	| Test Estate 2 | Safaricom | Safaricom Contract | Variable Topup | Custom      |        |

	When I add the following Transaction Fees
	| EstateName    | OperatorName | ContractDescription | ProductName    | CalculationType | FeeDescription      | Value |
	| Test Estate 1 | Safaricom    | Safaricom Contract  | Variable Topup | Fixed           | Merchant Commission | 2.50  |
	| Test Estate 2 | Safaricom    | Safaricom Contract  | Variable Topup | Percentage      | Merchant Commission | 0.85  |

	Given I create the following merchants
	| MerchantName    | AddressLine1   | Town     | Region      | Country        | ContactName    | EmailAddress                 | EstateName    |
	| Test Merchant 1 | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 1 | testcontact1@merchant1.co.uk | Test Estate 1 |
	| Test Merchant 2 | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 2 | testcontact2@merchant2.co.uk | Test Estate 1 |
	| Test Merchant 3 | Address Line 1 | TestTown | Test Region | United Kingdom | Test Contact 3 | testcontact3@merchant2.co.uk | Test Estate 2 |

	Given I have assigned the following  operator to the merchants
	| OperatorName | MerchantName    | MerchantNumber | TerminalNumber | EstateName    |
	| Safaricom    | Test Merchant 1 | 00000001       | 10000001       | Test Estate 1 |
	| Safaricom    | Test Merchant 2 | 00000002       | 10000002       | Test Estate 1 |
	| Safaricom    | Test Merchant 3 | 00000003       | 10000003       | Test Estate 2 |

	Given I have assigned the following devices to the merchants
	| DeviceIdentifier | MerchantName    | EstateName    |
	| 123456780        | Test Merchant 1 | Test Estate 1 |
	| 123456781        | Test Merchant 2 | Test Estate 1 |
	| 123456782        | Test Merchant 3 | Test Estate 2 |

	Given I make the following manual merchant deposits 
	| Reference | Amount   | DateTime | MerchantName    | EstateName    |
	| Deposit1  | 30000.00 | Today    | Test Merchant 1 | Test Estate 1 |
	| Deposit1  | 10000.00 | Today    | Test Merchant 2 | Test Estate 1 |
	| Deposit1  | 10000.00 | Today    | Test Merchant 3 | Test Estate 2 |

Scenario: Sales Transactions By Month - Transactions All On Same Day

	Given I set the date for today to "15/10/2020"

	When I perform the following transactions
	| DateTime | TransactionNumber | TransactionType | MerchantName    | DeviceIdentifier | EstateName    | OperatorName | TransactionAmount | CustomerAccountNumber | CustomerEmailAddress        | ContractDescription | ProductName    |
	| Today    | 1                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 1000.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 2                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 500.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 3                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 2500.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 4                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 1500.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 5                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 300.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 6                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 4000.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 7                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 6000.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 8                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 10000.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	
	| Today    | 1                 | Sale            | Test Merchant 2 | 123456781        | Test Estate 1 | Safaricom    | 1000.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 2                 | Sale            | Test Merchant 2 | 123456781        | Test Estate 1 | Safaricom    | 500.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 3                 | Sale            | Test Merchant 2 | 123456781        | Test Estate 1 | Safaricom    | 2500.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	| Today    | 4                 | Sale            | Test Merchant 2 | 123456781        | Test Estate 1 | Safaricom    | 1500.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |
	
	| Today    | 1                 | Sale            | Test Merchant 3 | 123456782        | Test Estate 2 | Safaricom    | 10000.00           | 123456789             |                             | Safaricom Contract   | Variable Topup |

	Then transaction response should contain the following information
	| EstateName    | MerchantName    | TransactionNumber | ResponseCode | ResponseMessage |
	| Test Estate 1 | Test Merchant 1 | 1                 | 0000         | SUCCESS         |
	| Test Estate 1 | Test Merchant 1 | 2                 | 1008         | DECLINED BY OPERATOR         |
	| Test Estate 1 | Test Merchant 1 | 3                 | 0000         | SUCCESS         |
	| Test Estate 1 | Test Merchant 1 | 4                 | 0000         | SUCCESS         |
	| Test Estate 1 | Test Merchant 1 | 5                 | 1008         | DECLINED BY OPERATOR         |
	| Test Estate 1 | Test Merchant 1 | 6                 | 0000         | SUCCESS         |
	| Test Estate 1 | Test Merchant 1 | 7                 | 0000         | SUCCESS         |
	| Test Estate 1 | Test Merchant 1 | 8                 | 0000         | SUCCESS         |

	| Test Estate 1 | Test Merchant 2 | 1                 | 0000         | SUCCESS         |
	| Test Estate 1 | Test Merchant 2 | 2                 | 1008         | DECLINED BY OPERATOR         |
	| Test Estate 1 | Test Merchant 2 | 3                 | 0000         | SUCCESS         |
	| Test Estate 1 | Test Merchant 2 | 4                 | 0000         | SUCCESS         |

	| Test Estate 2 | Test Merchant 3 | 1                 | 0000         | SUCCESS |

	When I get the Estate Transactions By Month Report for Estate 'Test Estate 1' with the Start Date 'Today' and the End Date 'Today' the following data is returned
	| MonthNumber | Year | NumberOfTransactions | ValueOfTransactions |
	| 10         | 2020 | 9                    | 30000.00            |

	When I get the Estate Transactions By Month Report for Estate 'Test Estate 2' with the Start Date 'Today' and the End Date 'Today' the following data is returned
	| MonthNumber | Year | NumberOfTransactions | ValueOfTransactions |
	| 10         | 2020 | 1                    | 10000.00            |

	When I get the Merchant Transactions By Month Report for Estate 'Test Estate 1' and Merchant 'Test Merchant 2' with the Start Date 'Today' and the End Date 'Today' the following data is returned
	| MonthNumber | Year | NumberOfTransactions | ValueOfTransactions |
	| 10         | 2020 | 3                    | 5000.00             |

@PRTest
Scenario: Sales Transactions By Month - Transactions On Mutiple Days

	Given I set the date for today to "05/10/2020"

	When I perform the following transactions
	| DateTime  | TransactionNumber | TransactionType | MerchantName    | DeviceIdentifier | EstateName    | OperatorName | TransactionAmount | CustomerAccountNumber | CustomerEmailAddress | ContractDescription | ProductName    |
	| LastMonth | 1                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 1000.00           | 123456789             |                      | Safaricom Contract  | Variable Topup |
	| LastWeek  | 2                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 500.00            | 123456789             |                      | Safaricom Contract  | Variable Topup |
	| LastWeek  | 3                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 2500.00           | 123456789             |                      | Safaricom Contract  | Variable Topup |
	| Yesterday | 4                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 1500.00           | 123456789             |                      | Safaricom Contract  | Variable Topup |
	| Yesterday | 5                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 300.00            | 123456789             |                      | Safaricom Contract  | Variable Topup |
	| Today     | 6                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 4000.00           | 123456789             |                      | Safaricom Contract  | Variable Topup |
	| Today     | 7                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 6000.00           | 123456789             |                      | Safaricom Contract  | Variable Topup |
	| Today     | 8                 | Sale            | Test Merchant 1 | 123456780        | Test Estate 1 | Safaricom    | 10000.00          | 123456789             |                      | Safaricom Contract  | Variable Topup |
	
	| Today | 1 | Sale | Test Merchant 2 | 123456781 | Test Estate 1 | Safaricom | 1000.00 | 123456789 |  | Safaricom Contract | Variable Topup |
	| Today | 2 | Sale | Test Merchant 2 | 123456781 | Test Estate 1 | Safaricom | 500.00  | 123456789 |  | Safaricom Contract | Variable Topup |
	| Today | 3 | Sale | Test Merchant 2 | 123456781 | Test Estate 1 | Safaricom | 2500.00 | 123456789 |  | Safaricom Contract | Variable Topup |
	| Today | 4 | Sale | Test Merchant 2 | 123456781 | Test Estate 1 | Safaricom | 1500.00 | 123456789 |  | Safaricom Contract | Variable Topup |
	
	| LastMonth | 1 | Sale | Test Merchant 3 | 123456782 | Test Estate 2 | Safaricom | 1000.00 | 123456789 |  | Safaricom Contract | Variable Topup |
	| LastWeek  | 2 | Sale | Test Merchant 3 | 123456782 | Test Estate 2 | Safaricom | 2000.00 | 123456789 |  | Safaricom Contract | Variable Topup |
	| Yesterday | 3 | Sale | Test Merchant 3 | 123456782 | Test Estate 2 | Safaricom | 1000.00 | 123456789 |  | Safaricom Contract | Variable Topup |
	| Today     | 4 | Sale | Test Merchant 3 | 123456782 | Test Estate 2 | Safaricom | 1000.00 | 123456789 |  | Safaricom Contract | Variable Topup |

	Then transaction response should contain the following information
	| EstateName    | MerchantName    | TransactionNumber | ResponseCode | ResponseMessage      |
	| Test Estate 1 | Test Merchant 1 | 1                 | 0000         | SUCCESS              |
	| Test Estate 1 | Test Merchant 1 | 2                 | 1008         | DECLINED BY OPERATOR |
	| Test Estate 1 | Test Merchant 1 | 3                 | 0000         | SUCCESS              |
	| Test Estate 1 | Test Merchant 1 | 4                 | 0000         | SUCCESS              |
	| Test Estate 1 | Test Merchant 1 | 5                 | 1008         | DECLINED BY OPERATOR |
	| Test Estate 1 | Test Merchant 1 | 6                 | 0000         | SUCCESS              |
	| Test Estate 1 | Test Merchant 1 | 7                 | 0000         | SUCCESS              |
	| Test Estate 1 | Test Merchant 1 | 8                 | 0000         | SUCCESS              |

	| Test Estate 1 | Test Merchant 2 | 1 | 0000 | SUCCESS              |
	| Test Estate 1 | Test Merchant 2 | 2 | 1008 | DECLINED BY OPERATOR |
	| Test Estate 1 | Test Merchant 2 | 3 | 0000 | SUCCESS              |
	| Test Estate 1 | Test Merchant 2 | 4 | 0000 | SUCCESS              |

	| Test Estate 2 | Test Merchant 3 | 1 | 0000 | SUCCESS |
	| Test Estate 2 | Test Merchant 3 | 2 | 0000 | SUCCESS |
	| Test Estate 2 | Test Merchant 3 | 3 | 0000 | SUCCESS |
	| Test Estate 2 | Test Merchant 3 | 4 | 0000 | SUCCESS |

	When I get the Estate Transactions By Month Report for Estate 'Test Estate 1' with the Start Date 'LastMonth' and the End Date 'Today' the following data is returned
	| MonthNumber | Year | NumberOfTransactions | ValueOfTransactions |
	| 9           | 2020 | 2                    | 3500.00             |
	| 10          | 2020 | 7                    | 26500.00            |

	When I get the Estate Transactions By Month Report for Estate 'Test Estate 2' with the Start Date 'LastMonth' and the End Date 'Today' the following data is returned
	| MonthNumber | Year | NumberOfTransactions | ValueOfTransactions |
	| 9           | 2020 | 2                    | 3000.00             |
	| 10          | 2020 | 2                    | 2000.00             |

	When I get the Merchant Transactions By Month Report for Estate 'Test Estate 1' and Merchant 'Test Merchant 2' with the Start Date 'LastMonth' and the End Date 'Today' the following data is returned
	| MonthNumber | Year | NumberOfTransactions | ValueOfTransactions |
	| 10         | 2020 | 3                    | 5000.00            |