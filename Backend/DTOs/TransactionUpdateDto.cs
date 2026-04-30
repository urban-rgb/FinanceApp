using System.ComponentModel.DataAnnotations;
using FinanceApp.Models;

namespace FinanceApp.DTOs;

public class TransactionUpdateDto
{
    [Required]
    [MinLength(3)]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [EnumDataType(typeof(TransactionType))]
    public TransactionType Type { get; set; }
}