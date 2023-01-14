using GFET.Enum;
using GFET.Helper;
using GFET.Models.Food;
using GFET.Models.Profile;
using GFET.Models.Property;
using GFET.Models.User;
using Microsoft.EntityFrameworkCore;

namespace GFET.Base;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    public DbSet<Food> Foods { get; set; }
        
    public DbSet<Profile> Profiles { get; set; }

    public DbSet<User> Users { get; set; }
        
    public DbSet<Basket> Baskets { get; set; }
        
    public DbSet<Property> Properties { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder =>
            {
                builder.ToTable("Users").HasKey(x => x.Id);
                builder.HasData(new User
                {
                    Id = 1,
                    Name = "admin",
                    Password = HashPasswordHelper.HashPassowrd("admin"),
                    Role = UserRole.Admin
                });

                builder.Property(x => x.Id).ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.Profile)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
                
                builder.HasOne(x => x.Basket)
                    .WithOne(x => x.User)
                    .HasPrincipalKey<User>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            modelBuilder.Entity<Food>(builder =>
            {
                builder.ToTable("Foods").HasKey(x => x.id);
                
                builder.HasData(new Food
                {
                    id = 1,
                    name = "GTPO",
                    description = new string('A', 50),
                    price = new decimal(),
                    Avatar = null,
                    CatagoryFood = CategoryFood.Pizza
                });
            });

            modelBuilder.Entity<Profile>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.id);
                
                builder.Property(x => x.id).ValueGeneratedOnAdd();
                builder.Property(x => x.Age);
                builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);
                
                builder.HasData(new Profile()
                {
                    id = 1,
                    UserId = 1
                });
            });
            
            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.id);
                
                builder.HasData(new Basket() 
                {
                    id = 1,
                    UserId = 1
                });
            });
            
            modelBuilder.Entity<Property>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.id);

                builder.HasOne(r => r.Basket).WithMany(t => t.Propertis)
                    .HasForeignKey(r => r.BasketId);
            });
        }
}