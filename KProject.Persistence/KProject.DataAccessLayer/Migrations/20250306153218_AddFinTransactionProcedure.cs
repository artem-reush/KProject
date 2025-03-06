using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KProject.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddFinTransactionProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
@"SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO
CREATE PROCEDURE dbo.ExecuteTransaction
	@UserID INT,
	@Currency INT,
	@IsIncomeTransaction BIT,
	@Summ NUMERIC(15, 2)
AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION

		IF @Summ < 0
			RAISERROR('Сумма не может быть отрицательной', 11, 1)

		UPDATE dbo.Accounts WITH (UPDLOCK, ROWLOCK)
			SET CurrentBalance = CurrentBalance + IIF(@IsIncomeTransaction = 1, @Summ, 0 - @Summ)
			WHERE UserID = @UserID AND Currency = @Currency
				AND (@IsIncomeTransaction = 1 OR CurrentBalance - @Summ >= 0)
	
		IF @@ROWCOUNT = 0
		BEGIN
			RAISERROR('Счёт не найден или недостаточно стредств', 11, 1)
		END

		INSERT INTO dbo.Transactions
			(UserID, Currency, Income, Withdraw)
		VALUES
			(@UserID, @Currency, IIF(@IsIncomeTransaction = 1, @Summ, 0), IIF(@IsIncomeTransaction = 0, @Summ, 0))

		COMMIT TRANSACTION
	END TRY
	BEGIN CATCH 
		ROLLBACK TRANSACTION

		DECLARE @ErrMsg NVARCHAR(2000) = ERROR_MESSAGE()
		RAISERROR(@ErrMsg, 11, 1)
	END CATCH
END
GO

");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP PROCEDURE dbo.ExecuteTransaction");
        }
    }
}
