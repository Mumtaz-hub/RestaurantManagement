select MONTH(bill_date) as 'Month',SUM(Amount) as 'Amount',SUM(Discount) as 'Discount',
SUM(Delivery_Charge) as 'Delivery_Charge', SUM(Amount)-SUM(Discount)+SUM(Delivery_Charge) as 'NetAmount'
from Bill_Master
group by MONTH(bill_date)

select Bill_Date,Bill_No,MONTH(bill_date) as 'Month',SUM(Amount) as 'Amount',SUM(Discount) as 'Discount',
SUM(Delivery_Charge) as 'Delivery_Charge', SUM(Amount)-SUM(Discount)+SUM(Delivery_Charge) as 'NetAmount'
from Bill_Master
group by Bill_date,Bill_No,MONTH(bill_date)

/*select  Bill_Date,Bill_no,Sum(Amount-Discount+Delivery_Charge) as 'NetAmount' 
from Bill_Master
group by Bill_Date,Bill_no*/
--having Sum(Amount-Discount+Delivery_Charge)>5000
