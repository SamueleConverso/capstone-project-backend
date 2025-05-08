using Capstone_Project.Models.Account;
using Capstone_Project.Models.Feed;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Capstone_Project.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers
        {
            get; set;
        }

        public DbSet<ApplicationRole> ApplicationRoles
        {
            get; set;
        }

        public DbSet<ApplicationUserRole> ApplicationUserRoles
        {
            get; set;
        }

        public DbSet<Post> Posts
        {
            get; set;
        }

        public DbSet<Tag> Tags
        {
            get; set;
        }

        public DbSet<PostTag> PostTags
        {
            get; set;
        }

        public DbSet<PostLike> PostLikes
        {
            get; set;
        }

        public DbSet<Comment> Comments
        {
            get; set;
        }

        public DbSet<CommentLike> CommentLikes
        {
            get; set;
        }

        public DbSet<Videogame> Videogames
        {
            get; set;
        }

        public DbSet<Review> Reviews
        {
            get; set;
        }

        public DbSet<Community> Communities
        {
            get; set;
        }

        public DbSet<CommunityApplicationUser> CommunityApplicationUsers
        {
            get; set;
        }

        public DbSet<Cart> Carts
        {
            get; set;
        }

        public DbSet<CartVideogame> CartVideogames
        {
            get; set;
        }

        public DbSet<FavouriteList> FavouriteList
        {
            get; set;
        }

        public DbSet<FavouriteVideogame> FavouriteVideogames
        {
            get; set;
        }

        public DbSet<FriendList> FriendList
        {
            get; set;
        }

        public DbSet<ApplicationUserFriend> ApplicationUserFriends
        {
            get; set;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.User).WithMany(u => u.ApplicationUserRoles).HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<ApplicationUserRole>().HasOne(ur => ur.Role).WithMany(r => r.ApplicationUserRole).HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<ApplicationUserRole>().Property(p => p.Date).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<ApplicationUser>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Post>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Tag>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<PostTag>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<PostLike>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Comment>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<CommentLike>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Videogame>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Review>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Community>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<CommunityApplicationUser>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<Cart>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<CartVideogame>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<FavouriteList>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<FavouriteVideogame>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<FriendList>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            modelBuilder.Entity<ApplicationUserFriend>().Property(p => p.CreatedAt).HasDefaultValueSql("GETDATE()").IsRequired(true);

            // Relazione tra Post e Community + se elimino una Community, elimino anche i suoi Post
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Community)
                .WithMany(c => c.Posts)
                .HasForeignKey(p => p.CommunityId)
                .OnDelete(DeleteBehavior.Cascade);

            //Tag unico per ogni post
            modelBuilder.Entity<PostTag>().HasIndex(pt => new { pt.PostId, pt.TagId }).IsUnique();

            //Like unico per ogni post
            modelBuilder.Entity<PostLike>().HasIndex(pl => new { pl.ApplicationUserId, pl.PostId }).IsUnique();

            //Like unico per ogni commento
            modelBuilder.Entity<CommentLike>()
                .HasIndex(cl => new { cl.ApplicationUserId, cl.CommentId })
                .IsUnique();

            //Videogioco unico per ogni carrello
            modelBuilder.Entity<CartVideogame>()
                .HasIndex(cv => new { cv.CartId, cv.VideogameId }).IsUnique();

            //Relazione 1:1 tra carrello e utente + carrello unico per ogni utente
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.Cart)
                .WithOne(c => c.ApplicationUser)
                .HasForeignKey<Cart>(c => c.ApplicationUserId);

            //Relazione 1:1 tra lista amici e utente + lista amici unica per ogni utente + se elimino un ApplicationUser, elimino anche la sua lista amici
            modelBuilder.Entity<FriendList>().HasOne(f => f.ApplicationUser).WithOne(u => u.FriendList).HasForeignKey<FriendList>(f => f.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);

            //Amico unico per ogni lista amici di ogni utente
            modelBuilder.Entity<ApplicationUserFriend>().HasIndex(uf => new { uf.ApplicationUserId, uf.FriendListId }).IsUnique();

            //Relazione 1:1 tra utente e lista videogiochi preferiti + lista videogiochi preferiti unica per ogni utente + se elimino un ApplicationUser, elimino anche la sua lista videogiochi preferiti
            modelBuilder.Entity<FavouriteList>()
                .HasOne(f => f.ApplicationUser)
                .WithOne(u => u.FavouriteList)
                .HasForeignKey<FavouriteList>(f => f.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);

            //Videogioco unico per ogni lista videogiochi preferiti
            modelBuilder.Entity<FavouriteVideogame>().HasIndex(fv => new { fv.FavouriteListId, fv.VideogameId }).IsUnique();

            //Utente unico per ogni comunità
            modelBuilder.Entity<CommunityApplicationUser>().HasIndex(ca => new { ca.ApplicationUserId, ca.CommunityId }).IsUnique();

            //Se elimino un ApplicationUser, non elimino automaticamente i suoi Post, ma setto a null la colonna ApplicationUserId
            modelBuilder.Entity<Post>().HasOne(p => p.ApplicationUser).WithMany(u => u.Posts).HasForeignKey(p => p.ApplicationUserId).OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Post>().HasOne(p => p.Videogame).WithMany(v => v.Posts).HasForeignKey(p => p.VideogameId);

            //Se elimino un Post, elimino anche i suoi PostTag
            modelBuilder.Entity<PostTag>().HasOne(pt => pt.Post).WithMany(p => p.PostTags).HasForeignKey(pt => pt.PostId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un Tag, elimino anche il suo PostTag e viceversa
            modelBuilder.Entity<PostTag>().HasOne(pt => pt.Tag).WithMany(t => t.PostTags).HasForeignKey(pt => pt.TagId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un ApplicationUser, elimino anche i suoi PostLike
            modelBuilder.Entity<PostLike>().HasOne(pl => pl.ApplicationUser).WithMany(u => u.PostLikes).HasForeignKey(pl => pl.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un Post, elimino anche i suoi PostLike
            modelBuilder.Entity<PostLike>().HasOne(pl => pl.Post).WithMany(p => p.PostLikes).HasForeignKey(pl => pl.PostId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un Post, elimino anche i suoi Comment
            modelBuilder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(c => c.PostId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un ApplicationUser, non elimino i suoi Comment perché i Comment vengono eliminati già da un'altra regola che stabilisce che se elimino un ApplicationUser elimino il Post e se elimino il Post elimino anche i Comment
            modelBuilder.Entity<Comment>().HasOne(c => c.ApplicationUser).WithMany(u => u.Comments).HasForeignKey(c => c.ApplicationUserId).OnDelete(DeleteBehavior.SetNull);

            //Se elimino un ApplicationUser, elimino anche i suoi CommentLike
            modelBuilder.Entity<CommentLike>().HasOne(cl => cl.ApplicationUser).WithMany(u => u.CommentLikes).HasForeignKey(cl => cl.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un Comment, elimino anche i suoi CommentLike
            modelBuilder.Entity<CommentLike>().HasOne(cl => cl.Comment).WithMany(c => c.CommentLikes).HasForeignKey(cl => cl.CommentId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un ApplicationUser, non elimino i suoi Videogame, ma setto a null la colonna ApplicationUserId
            modelBuilder.Entity<Videogame>().HasOne(v => v.ApplicationUser).WithMany(u => u.Videogames).HasForeignKey(v => v.ApplicationUserId).OnDelete(DeleteBehavior.SetNull);

            //Se elimino un ApplicationUser, elimino anche le sue Review
            modelBuilder.Entity<Review>().HasOne(r => r.ApplicationUser).WithMany(u => u.Reviews).HasForeignKey(r => r.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un Videogame, elimino anche le sue Review
            modelBuilder.Entity<Review>().HasOne(r => r.Videogame).WithMany(v => v.Reviews).HasForeignKey(r => r.VideogameId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un ApplicationUser, le sue Community rimangono, ma vengono settate a null le colonne ApplicationUserId
            modelBuilder.Entity<Community>().HasOne(c => c.ApplicationUser).WithMany(u => u.Communities).HasForeignKey(c => c.ApplicationUserId).OnDelete(DeleteBehavior.SetNull);

            //Se elimino un ApplicationUser, elimino anche le sue CommunityApplicationUser
            modelBuilder.Entity<CommunityApplicationUser>().HasOne(ca => ca.ApplicationUser).WithMany(u => u.CommunityApplicationUsers).HasForeignKey(ca => ca.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino una Community, elimino anche i suoi CommunityApplicationUser
            modelBuilder.Entity<CommunityApplicationUser>().HasOne(ca => ca.Community).WithMany(c => c.CommunityApplicationUsers).HasForeignKey(ca => ca.CommunityId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un ApplicationUser, elimino anche il suo Cart
            modelBuilder.Entity<Cart>().HasOne(c => c.ApplicationUser).WithOne(u => u.Cart).HasForeignKey<Cart>(c => c.ApplicationUserId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un ApplicationUser, elimino anche i suoi CartVideogame
            modelBuilder.Entity<CartVideogame>()
              .HasOne(cv => cv.Cart)
              .WithMany(c => c.CartVideogames)
              .HasForeignKey(cv => cv.CartId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un Videogame, elimino anche i suoi CartVideogame
            modelBuilder.Entity<CartVideogame>()
                .HasOne(cv => cv.Videogame)
                .WithMany(v => v.CartVideogames)
                .HasForeignKey(cv => cv.VideogameId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un ApplicationUser, elimino anche i suoi FavouriteVideogame
            modelBuilder.Entity<FavouriteVideogame>()
                .HasOne(fv => fv.FavouriteList)
                .WithMany(fl => fl.FavouriteVideogames)
                .HasForeignKey(fv => fv.FavouriteListId).OnDelete(DeleteBehavior.Cascade);

            //Se elimino un Videogame, elimino anche i suoi FavouriteVideogame
            modelBuilder.Entity<FavouriteVideogame>().HasOne(fv => fv.Videogame).WithMany(v => v.FavouriteVideogames).HasForeignKey(fv => fv.VideogameId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUserFriend>().HasOne(uf => uf.FriendList).WithMany(f => f.ApplicationUserFriends).HasForeignKey(uf => uf.FriendListId).OnDelete(DeleteBehavior.NoAction);

            //Se elimino un ApplicationUser, elimino anche i suoi ApplicationUserFriend
            modelBuilder.Entity<ApplicationUserFriend>().HasOne(uf => uf.ApplicationUser).WithMany(u => u.ApplicationUserFriends).HasForeignKey(uf => uf.ApplicationUserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}

