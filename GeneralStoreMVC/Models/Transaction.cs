using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GeneralStoreMVC.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        //[ForeignKey(nameof(CustomerId))]
        public int CustomerId { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer customer { get; set; }

        [Required]
        //[ForeignKey (nameof(Product))]
        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Product product { get; set; }

        [Required]
        public int PurchaseQuantity { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DateOfTransaction { get; set; }
    }
}