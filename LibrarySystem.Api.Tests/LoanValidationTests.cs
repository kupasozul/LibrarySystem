using LibrarySystem.Api.Entities;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace LibrarySystem.Api.Tests;

public class LoanValidationTests
{

    [Fact]
    public void Book_WhitespaceTitle_FailsValidation()
    {
        var book = new Book { Title = "   ", Author = "Szerző", Publisher = "Kiadó", ReleaseYear = 2020 };
        var errors = ValidateModel(book);
        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Book.Title)));
    }

    [Fact]
    public void Book_NegativeReleaseYear_FailsValidation()
    {
        var book = new Book { Title = "Cím", Author = "Szerző", Publisher = "Kiadó", ReleaseYear = -2026 };
        var errors = ValidateModel(book);
        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Book.ReleaseYear)));
    }
    

    [Fact]
    public void Reader_EmptyName_FailsValidation()
    {
        var reader = new Reader { Name = "", Address = "Cím", DateOfBirth = new DateTime(1990, 1, 1) };
        var errors = ValidateModel(reader);
        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Reader.Name)));
    }
    

    [Fact]
    public void Loan_ReturnDeadlineBeforeLoanDate_FailsValidation()
    {
        var loan = new Loan
        {
            LoanDate = DateTime.Today,
            ReturnDeadline = DateTime.Today.AddDays(-1)
        };

        var errors = ValidateLoan(loan);
        Assert.Contains(errors, e => e.MemberNames.Contains(nameof(Loan.ReturnDeadline)));
    }

    // --- Segédfüggvények (Ezek futtatják a validátort) ---

    private static List<ValidationResult> ValidateLoan(Loan loan)
    {
        var context = new ValidationContext(loan);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(loan, context, results, validateAllProperties: true);
        
        // Ez hívja meg a Loan.cs-ben lévő Validate() metódust
        var vResults = loan.Validate(context);
        if (vResults != null) results.AddRange(vResults);
        
        return results;
    }

    private static List<ValidationResult> ValidateModel(object model)
    {
        var context = new ValidationContext(model);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, validateAllProperties: true);
        return results;
    }
}