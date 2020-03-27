using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Project0.Library.DataAccess;

namespace Project0.App
{
    public partial class Project0DbContext : DbContext
    {
        public Project0DbContext()
        {
        }

        public Project0DbContext(DbContextOptions<Project0DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<OrderList> OrderList { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductOrder> ProductOrder { get; set; }
        public virtual DbSet<StoreInventory> StoreInventory { get; set; }
        public virtual DbSet<StoreLocation> StoreLocation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SecretConfig.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.CstmId)
                    .HasName("PK__Customer__E2758465AA002318");

                entity.Property(e => e.CstmId).HasColumnName("cstmId");

                entity.Property(e => e.CstmDefaultStoreLoc)
                    .HasColumnName("cstmDefaultStoreLoc")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CstmEmail)
                    .HasColumnName("cstmEmail")
                    .HasMaxLength(45);

                entity.Property(e => e.CstmFirstName)
                    .IsRequired()
                    .HasColumnName("cstmFirstName")
                    .HasMaxLength(35);

                entity.Property(e => e.CstmLastName)
                    .IsRequired()
                    .HasColumnName("cstmLastName")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<OrderList>(entity =>
            {
                entity.HasKey(e => e.LstId)
                    .HasName("PK__OrderLis__E5B98E6E7F0FA421");

                entity.Property(e => e.LstId).HasColumnName("lstId");

                entity.Property(e => e.LstOrderId).HasColumnName("lstOrderId");

                entity.Property(e => e.LstProdId).HasColumnName("lstProdId");

                entity.HasOne(d => d.LstOrder)
                    .WithMany(p => p.OrderList)
                    .HasForeignKey(d => d.LstOrderId)
                    .HasConstraintName("FK__OrderList__lstOr__5E8A0973");

                entity.HasOne(d => d.LstProd)
                    .WithMany(p => p.OrderList)
                    .HasForeignKey(d => d.LstProdId)
                    .HasConstraintName("FK__OrderList__lstPr__5F7E2DAC");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProdId)
                    .HasName("PK__Product__319F67F1035F13EA");

                entity.Property(e => e.ProdId).HasColumnName("prodId");

                entity.Property(e => e.ProdDescription)
                    .HasColumnName("prodDescription")
                    .HasMaxLength(75);

                entity.Property(e => e.ProdName)
                    .IsRequired()
                    .HasColumnName("prodName")
                    .HasMaxLength(40);

                entity.Property(e => e.ProdPrice)
                    .HasColumnName("prodPrice")
                    .HasColumnType("money");
            });

            modelBuilder.Entity<ProductOrder>(entity =>
            {
                entity.HasKey(e => e.OrderId)
                    .HasName("PK__ProductO__0809335D1C3ECFD9");

                entity.Property(e => e.OrderId).HasColumnName("orderId");

                entity.Property(e => e.OrderCstmId).HasColumnName("orderCstmId");

                entity.Property(e => e.OrderOrdDate)
                    .HasColumnName("orderOrdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderStrId).HasColumnName("orderStrID");

                entity.HasOne(d => d.OrderCstm)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.OrderCstmId)
                    .HasConstraintName("FK__ProductOr__order__56E8E7AB");

                entity.HasOne(d => d.OrderStr)
                    .WithMany(p => p.ProductOrder)
                    .HasForeignKey(d => d.OrderStrId)
                    .HasConstraintName("FK__ProductOr__order__57DD0BE4");
            });

            modelBuilder.Entity<StoreInventory>(entity =>
            {
                entity.HasKey(e => e.InvId)
                    .HasName("PK__StoreInv__10325540EB24472B");

                entity.Property(e => e.InvId)
                    .HasColumnName("invId")
                    .ValueGeneratedNever();

                entity.Property(e => e.InvProdId).HasColumnName("invProdId");

                entity.Property(e => e.InvProdInventory).HasColumnName("invProdInventory");

                entity.Property(e => e.InvStoreLoc).HasColumnName("invStoreLoc");

                entity.HasOne(d => d.InvProd)
                    .WithMany(p => p.StoreInventory)
                    .HasForeignKey(d => d.InvProdId)
                    .HasConstraintName("FK__StoreInve__invPr__5224328E");

                entity.HasOne(d => d.InvStoreLocNavigation)
                    .WithMany(p => p.StoreInventory)
                    .HasForeignKey(d => d.InvStoreLoc)
                    .HasConstraintName("FK__StoreInve__invSt__531856C7");
            });

            modelBuilder.Entity<StoreLocation>(entity =>
            {
                entity.HasKey(e => e.LocId)
                    .HasName("PK__StoreLoc__7931970B8617413B");

                entity.Property(e => e.LocId).HasColumnName("locId");

                entity.Property(e => e.LocCity)
                    .IsRequired()
                    .HasColumnName("locCity")
                    .HasMaxLength(50);

                entity.Property(e => e.LocCountry)
                    .IsRequired()
                    .HasColumnName("locCountry")
                    .HasMaxLength(50);

                entity.Property(e => e.LocStreet)
                    .IsRequired()
                    .HasColumnName("locStreet")
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
