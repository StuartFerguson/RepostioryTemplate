CREATE OR ALTER VIEW uvwSettlements
AS

SELECT
	s.SettlementId,
	s.SettlementDate,
	FORMAT(s.SettlementDate, 'dddd') as DayOfWeek,
	DATEPART(wk, t.TransactionDate) as WeekNumber,
	FORMAT(s.SettlementDate, 'MMMM') as Month,
	DATEPART(MM, s.SettlementDate) as MonthNumber,
	YEAR(s.SettlementDate) as YearNumber,
	f.CalculatedValue,
	f.TransactionId,
	t.EstateId,
	t.MerchantId,
	CASE t.OperatorIdentifier
		WHEN 'Voucher' THEN REPLACE(c.Description, ' Contract', '')
		ELSE COALESCE(t.OperatorIdentifier, '')
	END as OperatorIdentifier,
	CAST(ISNULL(tar.Amount,0) as decimal) as Amount
from settlement s 
inner join merchantsettlementfees f on s.SettlementId = f.SettlementId
inner join [transaction] t on t.TransactionId = f.TransactionId
left outer join transactionadditionalrequestdata tar on tar.TransactionId = t.TransactionId AND tar.MerchantId = t.MerchantId and tar.EstateId = t.EstateId
inner join contract c on c.ContractId = t.ContractId