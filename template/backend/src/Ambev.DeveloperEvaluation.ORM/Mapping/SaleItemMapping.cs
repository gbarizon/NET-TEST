﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemMapping : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("sale_items");

            builder.HasKey("ProductId", "SaleId");

            builder.Property(si => si.ProductId)
                .HasColumnName("product_id")
                .IsRequired();

            builder.Property<Guid>("SaleId")
                .HasColumnName("sale_id")
                .IsRequired();

            builder.Property(si => si.ProductName)
                .HasColumnName("product_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(si => si.Quantity)
                .HasColumnName("quantity")
                .IsRequired();

            builder.Property(si => si.UnitPrice)
                .HasColumnName("unit_price")
                .HasColumnType("decimal(10,2)")
                .IsRequired();

            builder.Property(si => si.Discount)
                .HasColumnName("discount")
                .HasColumnType("decimal(10,2)")
                .IsRequired();
        }
    }
}
