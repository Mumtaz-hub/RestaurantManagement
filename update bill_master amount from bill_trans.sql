--alter table bill_master
--add  Amount numeric(18,2)

select m.Bill_No,m.Amount from Bill_Master m
select d.Bill_No,(D.Qty*D.UnitPrice) as 'Amount' from Bill_Trans D


/*UPDATE Bill_Master
SET Amount = (t2.Qty*t2.UnitPrice)
FROM Bill_Master t1
INNER JOIN Bill_Trans t2 ON t1.Bill_No = t2.Bill_No
*/

-------Perfect Query
UPDATE Bill_Master
SET Amount1 = ( select Sum(t2.Qty*t2.UnitPrice) from Bill_Trans t2 where t2.Bill_No=t1.Bill_No)
FROM Bill_Master t1



