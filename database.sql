CREATE DATABASE dbPointOfSale;

USE dbPointOfSale
GO

--Creating tables for my database
CREATE TABLE tbCustomer
(
	Cust_ID INT PRIMARY KEY IDENTITY(1,1),
	FName VARCHAR(20) DEFAULT 'DEMO' NOT NULL,
	LName VARCHAR(20) DEFAULT 'Customer' NOT NULL,
	Cust_Contact VARCHAR(20) DEFAULT NULL
);


CREATE TABLE tbEmployee
(
	Emp_ID INT PRIMARY KEY IDENTITY(1,1),
	FName VARCHAR(20) NOT NULL,
	LName VARCHAR(20) DEFAULT '' NOT NULL,
	Emp_Designation VARCHAR(40) NOT NULL,
	Emp_Contact VARCHAR(20) NOT NULL
);

CREATE TABLE tbSupplier
(
	Supp_ID INT PRIMARY KEY IDENTITY(1,1),
	FName VARCHAR(20) DEFAULT 'DEMO' NOT NULL,
	LName VARCHAR(20) DEFAULT 'Supplier' NOT NULL,
	Supp_Contact VARCHAR(20) NOT NULL
);

CREATE TABLE tbProduct
(
	P_ID INT PRIMARY KEY IDENTITY(1,1),
	P_Dec VARCHAR(50) DEFAULT 'Product Description' NOT NULL,
	P_Price INT NOT NULL,
	P_Quantity INT NOT NULL
);

CREATE TABLE tbOrder
(
	O_ID INT PRIMARY KEY IDENTITY(1,1),
	Cust_ID INT NOT NULL FOREIGN KEY REFERENCES tbCustomer (Cust_ID),
	Emp_ID INT NOT NULL FOREIGN KEY REFERENCES tbEmployee (Emp_ID),
);

CREATE TABLE tbOrderDec
(
	O_ID INT NOT NULL FOREIGN KEY REFERENCES tbOrder (O_ID) ,
	P_ID INT NOT NULL FOREIGN KEY REFERENCES tbProduct (P_ID) ,
	Quantity INT DEFAULT 1 NOT NULL,
	PRIMARY KEY (O_ID, P_ID)
);

CREATE TABLE tbDelivery
(
	Dev_ID INT PRIMARY KEY IDENTITY(1,1),
	Supp_ID INT NOT NULL FOREIGN KEY REFERENCES tbSupplier (Supp_ID) ,
	Dev_Date DATE  NOT NULL,
	Dev_TIME TIME NOT NULL
);

CREATE TABLE tbDeliveryDec
(
	Dev_ID INT NOT NULL FOREIGN KEY REFERENCES tbProduct (P_ID) ,
	P_ID INT NOT NULL FOREIGN KEY REFERENCES tbProduct (P_ID) ,
	Pro_Quantity INT NOT NULL,
	PRIMARY KEY (Dev_ID, P_ID)
);

CREATE TABLE tbLogin
(
	Emp_ID INT NOT NULL FOREIGN KEY REFERENCES tbEmployee(Emp_ID) ON DELETE CASCADE, 
	Username VARCHAR(30) NOT NULL,
	User_password VARCHAR(30) NOT NULL,
	PRIMARY KEY (Emp_ID)
);

--Procedures for inserting, updating, deleting PRODUCTS
ALTER PROCEDURE spInsertUpdateDeleteProduct
(  
	@P_id INT = null,  
	@P_dec VARCHAR(50)= null , 
	@P_price INT = null, 
	@P_quantity INT = null,  
	@StatementType VARCHAR(20) = ''
)  
WITH ENCRYPTION
AS  
BEGIN
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRY
		BEGIN TRANSACTION
			IF @StatementType = 'Insert'  
				BEGIN  
				INSERT INTO tbProduct (P_Dec,P_Price,P_Quantity) 
				VALUES ( @P_dec,@P_price,@P_quantity) 
				COMMIT TRANSACTION
				END   
			ELSE IF @StatementType = 'Update'  
				BEGIN  
				UPDATE tbProduct 
				SET  P_Dec = @P_dec, P_Price = @P_price, P_Quantity = @P_quantity  
				WHERE P_ID = @P_id  
				COMMIT TRANSACTION
				END  
			ELSE IF @StatementType = 'Delete'  
				BEGIN  
				DELETE FROM tbProduct WHERE P_ID = @P_id  
				COMMIT TRANSACTION
				END  
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END  


--Procedures for inserting, updating, deleting Employee
ALTER PROCEDURE spInsertUpdateDeleteEmployee
(  
	@emp_id INT = null,  
	@fname VARCHAR(20)= null , 
	@lname VARCHAR(20) = null, 
	@emp_contact VARCHAR(20) = null,
	@emp_designation VARCHAR(50)= null ,  
	@StatementType VARCHAR(20) = ''
)  
WITH ENCRYPTION
AS  
BEGIN
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRY
		BEGIN TRANSACTION
			IF @StatementType = 'Insert'  
				BEGIN  
				INSERT INTO tbEmployee (FName,LName,Emp_Contact,Emp_Designation) 
				VALUES (@fname,@lname,@emp_contact,@emp_designation) 
				COMMIT TRANSACTION
				END   
			ELSE IF @StatementType = 'Update'  
				BEGIN  
				UPDATE tbEmployee 
				SET  FName = @fname, LName = @lname, Emp_Contact = @emp_contact, Emp_Designation = @emp_designation 
				WHERE Emp_ID = @emp_id  
				COMMIT TRANSACTION
				END  
			ELSE IF @StatementType = 'Delete'  
				BEGIN  
				DELETE FROM tbEmployee 
				WHERE Emp_ID = @emp_id 
				COMMIT TRANSACTION 
				END  
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END  

--Procedures for inserting, updating, deleting Supplier
ALTER PROCEDURE spInsertUpdateDeleteSupplier
(  
	@Supp_id INT = null,  
	@fname VARCHAR(20)= null , 
	@lname VARCHAR(20) = null, 
	@supp_contact VARCHAR(20) = null, 
	@StatementType VARCHAR(20) = ''
)  
WITH ENCRYPTION
AS  
BEGIN
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRY
		BEGIN TRANSACTION
			IF @StatementType = 'Insert'  
				BEGIN  
				INSERT INTO tbSupplier (FName,LName,Supp_Contact) VALUES (@fname,@lname,@supp_contact) 
				COMMIT TRANSACTION
				END   
			ELSE IF @StatementType = 'Update'  
				BEGIN  
				UPDATE tbSupplier
				SET  FName = @fname, LName = @lname, Supp_Contact = @supp_contact 
				WHERE Supp_ID = @supp_id  
				COMMIT TRANSACTION
				END  
			ELSE IF @StatementType = 'Delete'  
				BEGIN  
				DELETE FROM tbSupplier 
				WHERE Supp_ID = @supp_id  
				COMMIT TRANSACTION
				END  
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END  

--Procedures for inserting, updating, deleting Logins
ALTER PROCEDURE spInsertUpdateDeleteLogins
(  
	@emp_id INT = null,  
	@username VARCHAR(30)= null , 
	@password VARCHAR(30) = null,  
	@StatementType VARCHAR(20) = ''
)
WITH ENCRYPTION  
AS  
BEGIN
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRY
		BEGIN TRANSACTION
			IF @StatementType = 'Insert'  
				BEGIN  
				INSERT INTO tbLogin(Emp_ID,Username,User_password) VALUES (@emp_id,@username,@password) 
				COMMIT TRANSACTION
				END   
			ELSE IF @StatementType = 'Update'  
				BEGIN  
				UPDATE tbLogin
				SET  Username = @username, User_Password = @password 
				WHERE Emp_ID = @emp_id  
				COMMIT TRANSACTION
				END  
			ELSE IF @StatementType = 'Delete'  
				BEGIN  
				DELETE FROM tbLogin 
				WHERE Emp_ID = @emp_id 
				COMMIT TRANSACTION 
				END  
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END  


--Procedures for inserting, updating, deleting Logins
CREATE PROCEDURE spInsertUpdateDeleteDeliveryDec
(  
	@Dev_id INT = null,  
	@P_ID INT= null , 
	@Quantity INT = null,  
	@StatementType VARCHAR(20) = ''
)
WITH ENCRYPTION  
AS  
BEGIN
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
	BEGIN TRY
		BEGIN TRANSACTION
			IF @StatementType = 'Insert'  
				BEGIN  
				INSERT INTO tbDeliveryDec(Dev_ID,P_ID,Pro_Quantity) VALUES (@Dev_id,@P_ID,@Quantity) 
				COMMIT TRANSACTION
				END   
			ELSE IF @StatementType = 'Update'  
				BEGIN  
				UPDATE tbLogin
				SET  Username = @username, User_Password = @password 
				WHERE Emp_ID = @emp_id  
				COMMIT TRANSACTION
				END  
			ELSE IF @StatementType = 'Delete'  
				BEGIN  
				DELETE FROM tbLogin 
				WHERE Emp_ID = @emp_id 
				COMMIT TRANSACTION 
				END  
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END  

--creating funtion on tbLogin
ALTER FUNCTION fn_Login
(
	@user VARCHAR(30),
	@password VARCHAR(40)
)
RETURNS INT
WITH ENCRYPTION
AS 
BEGIN
	DECLARE @Found INT
	SET @Found =(SELECT Emp_ID FROM dbo.tbLogin 
				WHERE Username = @user AND User_password = @password)
	IF(@Found IS NULL)
		SET @Found = 0;
	ElSE
		SET @Found = 1;
	RETURN @Found

-- searches for value in table
CREATE PROCEDURE spSearchObject
	@tableName	VARCHAR(20), 
	@match INT OUTPUT
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION
			DECLARE @sql VARCHAR(MAX)
			SET @sql = 'SELECT * from ' + QUOTENAME(@tableName)
			EXECUTE sp_executesql @sql
			IF @@ROWCOUNT>0 
				PRINT '1'
			ELSE
				SET @match = 0
				print @match
			RETURN @match
		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH
		ROLLBACK TRANSACTION
	END CATCH
END
