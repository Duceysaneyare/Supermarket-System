Create database supermarket

use supermarket
go

create table categories(
catID int primary key not null,
catName varchar(60) not null,
catDesc varchar(50)
)

select * from categories

insert into categories values (
3,'Desktop', 'Second Desktop')
delete  from categories where catID =0; 


create table products(
ProdID int primary key not null,
ProdName varchar(50) not null,
ProdQty int not null,
ProdPrice varchar(50) not null,
ProdCat varchar(50) not null
)

select * from products

delete from products where ProdID = 11;


create table sellers(
SellerID int primary key not null,
SellerName varchar(50) not null,
SellerAge int not null,
SellerMobile varchar(50) not null,
Password varchar(50) not null
)

select * from sellers


create table bills(
BillID int primary key not null,
SellerName varchar(50) not null,
BillDate varchar(50) not null,
TotalAmount varchar(50) not null
)
delete from bills where BillID = 2

select * from bills