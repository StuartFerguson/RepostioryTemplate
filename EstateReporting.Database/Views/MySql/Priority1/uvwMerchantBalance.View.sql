CREATE OR REPLACE VIEW uvwMerchantBalance
AS
SELECT 
	m.EventId,
	m.EstateId,
	m.MerchantId,
	m.Balance,
	m.ChangeAmount,	
	CASE WHEN m.ChangeAmount <= 0 THEN 'D' ELSE 'C' END as EntryType,
	CASE WHEN m.ChangeAmount <= 0 THEN m.ChangeAmount ELSE NULL END as `Out`,
	CASE WHEN m.ChangeAmount > 0 THEN m.ChangeAmount ELSE NULL END as `In`,
	CASE m.Reference 
		WHEN 'Transaction Fee Processed' THEN DATE_ADD(t.TransactionDateTime, INTERVAL 5 SECOND)
		ELSE m.EntryDateTime
	END AS EntryDateTime,
	CASE m.Reference 
		WHEN 'Transaction Completed' THEN 
			CASE t.OperatorIdentifier 
				WHEN 'Voucher' THEN REPLACE(c.Description, ' Contract','') + ' Voucher (Txn #' + t.TransactionNumber + ')'
				ELSE t.OperatorIdentifier + ' Transaction (Txn #' + t.TransactionNumber + ')'
				END
		WHEN 'Transaction Fee Processed' THEN cptf.Description + ' (Txn #' + t.TransactionNumber + ')'
		ELSE m.Reference
	END as Reference,
	m.TransactionId
from merchantbalancehistory m
left outer join transaction t on t.TransactionId = m.TransactionId
left outer join transactionfee f on f.TransactionId = m.TransactionId and f.EventId = m.EventId
left outer join contractproducttransactionfee cptf on f.FeeId = cptf.TransactionFeeId
left outer join contract c on c.ContractId = t.ContractId