using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DailyQuest_v01.Models;

public partial class DailyQuestDbContext : DbContext
{
    public DailyQuestDbContext()
    {
    }

    public DailyQuestDbContext(DbContextOptions<DailyQuestDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentsLike> CommentsLikes { get; set; }

    public virtual DbSet<ConversationHistory> ConversationHistories { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<FriendRequestStatus> FriendRequestStatuses { get; set; }

    public virtual DbSet<FriendType> FriendTypes { get; set; }

    public virtual DbSet<ListOfParticipant> ListOfParticipants { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberAndRoleLook> MemberAndRoleLooks { get; set; }

    public virtual DbSet<MemberAndTool> MemberAndTools { get; set; }

    public virtual DbSet<MemberAndVirtualRole> MemberAndVirtualRoles { get; set; }

    public virtual DbSet<MemberCheckOut> MemberCheckOuts { get; set; }

    public virtual DbSet<MemberFriend> MemberFriends { get; set; }

    public virtual DbSet<MemberFriendship> MemberFriendships { get; set; }

    public virtual DbSet<MemberLevel> MemberLevels { get; set; }

    public virtual DbSet<MemberLoginHistory> MemberLoginHistories { get; set; }

    public virtual DbSet<MemberStatus> MemberStatuses { get; set; }

    public virtual DbSet<MessageStatus> MessageStatuses { get; set; }

    public virtual DbSet<Mission> Missions { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostCategory> PostCategories { get; set; }

    public virtual DbSet<PostsLike> PostsLikes { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<ReportCategory> ReportCategories { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleLook> RoleLooks { get; set; }

    public virtual DbSet<ScreenLook> ScreenLooks { get; set; }

    public virtual DbSet<ShopAdmin> ShopAdmins { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<SubTask> SubTasks { get; set; }

    public virtual DbSet<TaskLabel> TaskLabels { get; set; }

    public virtual DbSet<TaskResult> TaskResults { get; set; }

    public virtual DbSet<TaskType> TaskTypes { get; set; }

    public virtual DbSet<Title> Titles { get; set; }

    public virtual DbSet<TitleType> TitleTypes { get; set; }

    public virtual DbSet<Tool> Tools { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAndTaskR> UserAndTaskRs { get; set; }

    public virtual DbSet<UserAndTitleR> UserAndTitleRs { get; set; }

    public virtual DbSet<UsersLoginLog> UsersLoginLogs { get; set; }

    public virtual DbSet<UsersRole> UsersRoles { get; set; }

    public virtual DbSet<VirtualRole> VirtualRoles { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DailyQuestDB;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Member).WithMany(p => p.Admins)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admins_MemberID");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__City__031491A8D218E57B");

            entity.ToTable("City");

            entity.HasIndex(e => e.CityName, "UQ__City__1AA4F7B53FB1A38E").IsUnique();

            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasMaxLength(20)
                .HasColumnName("city_name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.CommentedAt).HasColumnType("datetime");
            entity.Property(e => e.CommentsContent).HasColumnType("text");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ParentCommentId).HasColumnName("ParentCommentID");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.Member).WithMany(p => p.Comments)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_MemberID");

            entity.HasOne(d => d.ParentComment).WithMany(p => p.InverseParentComment)
                .HasForeignKey(d => d.ParentCommentId)
                .HasConstraintName("FK_Comments_Comments");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Posts");
        });

        modelBuilder.Entity<CommentsLike>(entity =>
        {
            entity.HasKey(e => e.LikesId);

            entity.ToTable("Comments_Likes");

            entity.HasIndex(e => new { e.MemberId, e.CommentId }, "IX_Comments_Likes").IsUnique();

            entity.Property(e => e.LikesId).HasColumnName("LikesID");
            entity.Property(e => e.CommentId).HasColumnName("CommentID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentsLikes)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Likes_Comments");

            entity.HasOne(d => d.Member).WithMany(p => p.CommentsLikes)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Likes_MemberID");
        });

        modelBuilder.Entity<ConversationHistory>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Conversa__0BBF6EE6BD5BD50C");

            entity.ToTable("ConversationHistory");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.MessageContent).HasColumnName("message_content");
            entity.Property(e => e.ReceiverMemberId).HasColumnName("receiver_MemberID");
            entity.Property(e => e.ReplyMessageId).HasColumnName("reply_message_id");
            entity.Property(e => e.SendTime)
                .HasColumnType("datetime")
                .HasColumnName("send_time");
            entity.Property(e => e.SenderMemberId).HasColumnName("sender_MemberID");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.ReceiverMember).WithMany(p => p.ConversationHistoryReceiverMembers)
                .HasForeignKey(d => d.ReceiverMemberId)
                .HasConstraintName("FK__Conversat__recei__628FA481");

            entity.HasOne(d => d.ReplyMessage).WithMany(p => p.InverseReplyMessage)
                .HasForeignKey(d => d.ReplyMessageId)
                .HasConstraintName("FK__Conversat__reply__6477ECF3");

            entity.HasOne(d => d.SenderMember).WithMany(p => p.ConversationHistorySenderMembers)
                .HasForeignKey(d => d.SenderMemberId)
                .HasConstraintName("FK__Conversat__sende__619B8048");

            entity.HasOne(d => d.Status).WithMany(p => p.ConversationHistories)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__Conversat__statu__6383C8BA");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasIndex(e => new { e.MemberId, e.PostId }, "IX_Favorites").IsUnique();

            entity.Property(e => e.FavoriteId).HasColumnName("FavoriteID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.Member).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_MemberID");

            entity.HasOne(d => d.Post).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Favorites_Posts");
        });

        modelBuilder.Entity<FriendRequestStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__FriendRe__3683B53139AAE9AB");

            entity.ToTable("FriendRequestStatus");

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<FriendType>(entity =>
        {
            entity.HasKey(e => e.FriendTypeId).HasName("PK__FriendTy__539F4FFF4E8E4BA5");

            entity.ToTable("FriendType");

            entity.HasIndex(e => e.FriendTypeName, "UQ__FriendTy__179FD03148607493").IsUnique();

            entity.Property(e => e.FriendTypeId).HasColumnName("FriendTypeID");
            entity.Property(e => e.FriendTypeName).HasMaxLength(20);
        });

        modelBuilder.Entity<ListOfParticipant>(entity =>
        {
            entity.HasKey(e => e.TaskId);

            entity.Property(e => e.TaskId).ValueGeneratedNever();

            entity.HasOne(d => d.Participant).WithMany(p => p.ListOfParticipants)
                .HasForeignKey(d => d.ParticipantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ListOfParticipants_Member");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__Member__0CF04B384A30453A");

            entity.ToTable("Member");

            entity.HasIndex(e => e.Email, "UQ__Member__AB6E61643139AB85").IsUnique();

            entity.HasIndex(e => e.Phone, "UQ__Member__B43B145FB71B80E2").IsUnique();

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("avatar_url");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.LastPasswordHash)
                .HasMaxLength(255)
                .HasColumnName("LastPassword_hash");
            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(255)
                .HasColumnName("password_salt");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Points)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("points");
            entity.Property(e => e.RegisterTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("register_time");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Address).WithMany(p => p.Members)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_Member_City");

            entity.HasOne(d => d.Level).WithMany(p => p.Members)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("FK_Member_MemberLevel");

            entity.HasOne(d => d.Role).WithMany(p => p.Members)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Member_Role");

            entity.HasOne(d => d.Status).WithMany(p => p.Members)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK_Member_MemberStatus");
        });

        modelBuilder.Entity<MemberAndRoleLook>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MemberAndRoleLook");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.LookId).HasColumnName("LookID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Look).WithMany()
                .HasForeignKey(d => d.LookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberAndRoleLook_RoleLook");
        });

        modelBuilder.Entity<MemberAndTool>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MemberAndTool");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.OtherMemberId).HasColumnName("OtherMemberID");
            entity.Property(e => e.ToolId).HasColumnName("ToolID");

            entity.HasOne(d => d.Member).WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberAndTool_Member");

            entity.HasOne(d => d.OtherMember).WithMany()
                .HasForeignKey(d => d.OtherMemberId)
                .HasConstraintName("FK_MemberAndToolb_Member");

            entity.HasOne(d => d.Tool).WithMany()
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberAndTool_Tool");
        });

        modelBuilder.Entity<MemberAndVirtualRole>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MemberAndVirtualRole");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.Member).WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberAndVirtualRole_Member");

            entity.HasOne(d => d.Role).WithMany()
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberAndVirtualRole_VirtualRole");
        });

        modelBuilder.Entity<MemberCheckOut>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MemberCheckOut");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ToolId).HasColumnName("ToolID");

            entity.HasOne(d => d.Member).WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberCheckOut_Member");

            entity.HasOne(d => d.Tool).WithMany()
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberCheckOut_Tool");
        });

        modelBuilder.Entity<MemberFriend>(entity =>
        {
            entity.HasKey(e => new { e.MemberIdA, e.MemberIdB }).HasName("PK__MemberFr__E8B1E1CF269F82AC");

            entity.ToTable("MemberFriend");

            entity.Property(e => e.MemberIdA).HasColumnName("MemberID_a");
            entity.Property(e => e.MemberIdB).HasColumnName("MemberID_b");
            entity.Property(e => e.FriendTypeId).HasColumnName("FriendTypeID");
            entity.Property(e => e.RequestTime).HasColumnType("datetime");

            entity.HasOne(d => d.FriendType).WithMany(p => p.MemberFriends)
                .HasForeignKey(d => d.FriendTypeId)
                .HasConstraintName("FK__MemberFri__Frien__5812160E");

            entity.HasOne(d => d.MemberIdANavigation).WithMany(p => p.MemberFriendMemberIdANavigations)
                .HasForeignKey(d => d.MemberIdA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberFri__Membe__5629CD9C");

            entity.HasOne(d => d.MemberIdBNavigation).WithMany(p => p.MemberFriendMemberIdBNavigations)
                .HasForeignKey(d => d.MemberIdB)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberFri__Membe__571DF1D5");
        });

        modelBuilder.Entity<MemberFriendship>(entity =>
        {
            entity.HasKey(e => new { e.MemberIdA, e.MemberIdB }).HasName("PK__MemberFr__E8B1E1CFC4114511");

            entity.ToTable("MemberFriendship");

            entity.Property(e => e.MemberIdA).HasColumnName("MemberID_a");
            entity.Property(e => e.MemberIdB).HasColumnName("MemberID_b");
            entity.Property(e => e.RequestTime)
                .HasColumnType("datetime")
                .HasColumnName("request_time");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.MemberIdANavigation).WithMany(p => p.MemberFriendshipMemberIdANavigations)
                .HasForeignKey(d => d.MemberIdA)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberFri__Membe__46E78A0C");

            entity.HasOne(d => d.MemberIdBNavigation).WithMany(p => p.MemberFriendshipMemberIdBNavigations)
                .HasForeignKey(d => d.MemberIdB)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberFri__Membe__47DBAE45");

            entity.HasOne(d => d.Status).WithMany(p => p.MemberFriendships)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("FK__MemberFri__statu__48CFD27E");
        });

        modelBuilder.Entity<MemberLevel>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__MemberLe__0346164351D088AA");

            entity.ToTable("MemberLevel");

            entity.HasIndex(e => e.LevelName, "UQ__MemberLe__F94299E90A018E7E").IsUnique();

            entity.Property(e => e.LevelId).HasColumnName("level_id");
            entity.Property(e => e.CreationTime)
                .HasColumnType("datetime")
                .HasColumnName("creation_time");
            entity.Property(e => e.ExperienceNeeded).HasColumnName("experience_needed");
            entity.Property(e => e.LevelDescription)
                .HasMaxLength(255)
                .HasColumnName("level_description");
            entity.Property(e => e.LevelName)
                .HasMaxLength(50)
                .HasColumnName("level_name");
        });

        modelBuilder.Entity<MemberLoginHistory>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__MemberLo__9E2397E00533E37C");

            entity.ToTable("MemberLoginHistory");

            entity.Property(e => e.LogId).HasColumnName("log_id");
            entity.Property(e => e.DeviceInfo)
                .HasMaxLength(255)
                .HasColumnName("device_info");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.LoginSuccessFlag).HasColumnName("login_success_flag");
            entity.Property(e => e.LoginTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("login_time");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberLoginHistories)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MemberLog__Membe__4222D4EF");
        });

        modelBuilder.Entity<MemberStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__MemberSt__C8EE204345F55E89");

            entity.ToTable("MemberStatus");

            entity.HasIndex(e => e.StatusName, "UQ__MemberSt__05E7698A9874085C").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.StatusName).HasMaxLength(20);
        });

        modelBuilder.Entity<MessageStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__MessageS__3683B531259D1B68");

            entity.ToTable("MessageStatus");

            entity.HasIndex(e => e.StatusName, "UQ__MessageS__501B375393E55DC2").IsUnique();

            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(20)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Mission>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK_Task");

            entity.ToTable("Mission");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectDate).HasColumnType("datetime");
            entity.Property(e => e.FinishDate).HasColumnType("datetime");
            entity.Property(e => e.SetPeriod).HasMaxLength(50);
            entity.Property(e => e.TaskContent).HasMaxLength(50);

            entity.HasOne(d => d.SubTask).WithMany(p => p.Missions)
                .HasForeignKey(d => d.SubTaskId)
                .HasConstraintName("FK_Task_SubTask");

            entity.HasOne(d => d.TaskLabel).WithMany(p => p.Missions)
                .HasForeignKey(d => d.TaskLabelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_TaskLabel");

            entity.HasOne(d => d.TaskResult).WithMany(p => p.Missions)
                .HasForeignKey(d => d.TaskResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_TaskResult");

            entity.HasOne(d => d.TaskType).WithMany(p => p.Missions)
                .HasForeignKey(d => d.TaskTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_TaskType");

            entity.HasOne(d => d.Tool).WithMany(p => p.Missions)
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Task_Tool");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PostImage).HasColumnType("image");
            entity.Property(e => e.PostsContent).HasColumnType("text");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_PostCategories");

            entity.HasOne(d => d.Member).WithMany(p => p.Posts)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_MemberID");
        });

        modelBuilder.Entity<PostCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);

            entity.HasIndex(e => e.CategoryName, "IX_PostCategories").IsUnique();

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<PostsLike>(entity =>
        {
            entity.HasKey(e => e.LikesId);

            entity.ToTable("Posts_Likes");

            entity.HasIndex(e => new { e.MemberId, e.PostId }, "IX_Posts_Likes").IsUnique();

            entity.Property(e => e.LikesId).HasColumnName("LikesID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PostId).HasColumnName("PostID");

            entity.HasOne(d => d.Member).WithMany(p => p.PostsLikes)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_Likes_MemberID");

            entity.HasOne(d => d.Post).WithMany(p => p.PostsLikes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Posts_Likes_Posts");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.Property(e => e.ReportId).HasColumnName("ReportID");
            entity.Property(e => e.AdminComment).HasColumnType("text");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.ProcessedAt).HasColumnType("datetime");
            entity.Property(e => e.ReportCategoryId).HasColumnName("ReportCategoryID");
            entity.Property(e => e.ReportContent).HasColumnType("text");
            entity.Property(e => e.ReportedAt).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Admin).WithMany(p => p.Reports)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_Reports_Admins");

            entity.HasOne(d => d.Member).WithMany(p => p.Reports)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_MemberID");

            entity.HasOne(d => d.Post).WithMany(p => p.Reports)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_Posts");

            entity.HasOne(d => d.ReportCategory).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReportCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_ReportCategories");
        });

        modelBuilder.Entity<ReportCategory>(entity =>
        {
            entity.HasIndex(e => e.ReportCategoryName, "IX_ReportCategories").IsUnique();

            entity.Property(e => e.ReportCategoryId).HasColumnName("ReportCategoryID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ReportCategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE3A453E33F3");

            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleName, "UQ__Role__8A2B6160AFCBEDA0").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<RoleLook>(entity =>
        {
            entity.HasKey(e => e.LookId);

            entity.ToTable("RoleLook");

            entity.Property(e => e.LookId).HasColumnName("LookID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.LookDescription).HasMaxLength(50);
            entity.Property(e => e.LookName).HasMaxLength(50);
            entity.Property(e => e.LookPhoto).HasColumnType("image");
        });

        modelBuilder.Entity<ScreenLook>(entity =>
        {
            entity.HasKey(e => e.ScreenId);

            entity.ToTable("ScreenLook");

            entity.Property(e => e.ScreenId).HasColumnName("ScreenID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ScreenDescription).HasMaxLength(50);
            entity.Property(e => e.ScreenName).HasMaxLength(50);
            entity.Property(e => e.ScreenPhoto).HasColumnType("image");
        });

        modelBuilder.Entity<ShopAdmin>(entity =>
        {
            entity.HasKey(e => e.AdminId);

            entity.ToTable("ShopAdmin");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Member).WithMany(p => p.ShopAdmins)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShopAdmin_Member");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ShoppingCart");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ToolId).HasColumnName("ToolID");

            entity.HasOne(d => d.Member).WithMany()
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShoppingCart_Member");

            entity.HasOne(d => d.Tool).WithMany()
                .HasForeignKey(d => d.ToolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShoppingCart_Tool");
        });

        modelBuilder.Entity<SubTask>(entity =>
        {
            entity.ToTable("SubTask");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ExpectDate).HasColumnType("datetime");
            entity.Property(e => e.FinishDate).HasColumnType("datetime");
            entity.Property(e => e.TaskContent).HasMaxLength(50);

            entity.HasOne(d => d.Task).WithMany(p => p.SubTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubTask_Task");
        });

        modelBuilder.Entity<TaskLabel>(entity =>
        {
            entity.ToTable("TaskLabel");

            entity.Property(e => e.TaskLabelName).HasMaxLength(50);
        });

        modelBuilder.Entity<TaskResult>(entity =>
        {
            entity.ToTable("TaskResult");

            entity.Property(e => e.TaskResultName).HasMaxLength(50);
        });

        modelBuilder.Entity<TaskType>(entity =>
        {
            entity.ToTable("TaskType");

            entity.Property(e => e.TaskTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Title>(entity =>
        {
            entity.ToTable("Title");

            entity.Property(e => e.FinishDate).HasColumnType("datetime");
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.TitleName).HasMaxLength(50);

            entity.HasOne(d => d.TitleType).WithMany(p => p.Titles)
                .HasForeignKey(d => d.TitleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Title_TitleType");

            entity.HasOne(d => d.Tool).WithMany(p => p.Titles)
                .HasForeignKey(d => d.ToolId)
                .HasConstraintName("FK_Title_Tool");
        });

        modelBuilder.Entity<TitleType>(entity =>
        {
            entity.ToTable("TitleType");

            entity.Property(e => e.TitleCondition).HasMaxLength(50);
            entity.Property(e => e.TitleTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Tool>(entity =>
        {
            entity.ToTable("Tool");

            entity.Property(e => e.ToolId).HasColumnName("ToolID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.ToolDescription).HasMaxLength(50);
            entity.Property(e => e.ToolName).HasMaxLength(50);
            entity.Property(e => e.ToolPhoto).HasColumnType("image");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__SuperAdm__9D74425202ED6513");

            entity.HasIndex(e => e.Email, "UQ__SuperAdm__AB6E61646564B9E9").IsUnique();

            entity.HasIndex(e => e.Username, "UQ__SuperAdm__F3DBC572F9148E39").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Is2Faenabled).HasColumnName("Is2FAEnabled");
            entity.Property(e => e.LastLoginTime)
                .HasColumnType("datetime")
                .HasColumnName("last_login_time");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PasswordSalt)
                .HasMaxLength(255)
                .HasColumnName("password_salt");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleUserRel",
                    r => r.HasOne<UsersRole>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Role_User__permi__7B264821"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Role_User__UserI__7A3223E8"),
                    j =>
                    {
                        j.HasKey("UserId", "PermissionId").HasName("PK__Role_Use__A9DBFD032B108EC4");
                        j.ToTable("Role_User_Rel");
                        j.IndexerProperty<int>("UserId").HasColumnName("UserID");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("permission_id");
                    });
        });

        modelBuilder.Entity<UserAndTaskR>(entity =>
        {
            entity.HasKey(e => e.MemberId);

            entity.ToTable("UserAndTaskRS");

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("MemberID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithOne(p => p.UserAndTaskR)
                .HasForeignKey<UserAndTaskR>(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAndTaskRS_MemberID_Users");

            entity.HasOne(d => d.Task).WithMany(p => p.UserAndTaskRs)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAndTaskRS_Task");

            entity.HasOne(d => d.TaskResult).WithMany(p => p.UserAndTaskRs)
                .HasForeignKey(d => d.TaskResultId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAndTaskRS_TaskResult");
        });

        modelBuilder.Entity<UserAndTitleR>(entity =>
        {
            entity.HasKey(e => e.MemberId);

            entity.ToTable("UserAndTitleRS");

            entity.Property(e => e.MemberId)
                .ValueGeneratedNever()
                .HasColumnName("MemberID");
            entity.Property(e => e.FinishDate).HasColumnType("datetime");

            entity.HasOne(d => d.Member).WithOne(p => p.UserAndTitleR)
                .HasForeignKey<UserAndTitleR>(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAndTitleRS_MemberID_Users");

            entity.HasOne(d => d.Title).WithMany(p => p.UserAndTitleRs)
                .HasForeignKey(d => d.TitleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserAndTitleRS_Title");
        });

        modelBuilder.Entity<UsersLoginLog>(entity =>
        {
            entity.HasKey(e => e.LoginId).HasName("PK__SuperAdm__C2C971DBF3E3A1AF");

            entity.ToTable("UsersLoginLog");

            entity.Property(e => e.LoginId).HasColumnName("login_id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(50)
                .HasColumnName("ip_address");
            entity.Property(e => e.LoginTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("login_time");
            entity.Property(e => e.LogoutTime)
                .HasColumnType("datetime")
                .HasColumnName("logout_time");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.UsersLoginLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__SuperAdmi__Super__6D0D32F4");
        });

        modelBuilder.Entity<UsersRole>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__SuperAdm__E5331AFA7AA53FF5");

            entity.HasIndex(e => e.PermissionName, "UQ__SuperAdm__81C0F5A2004C3587").IsUnique();

            entity.Property(e => e.PermissionId).HasColumnName("permission_id");
            entity.Property(e => e.PermissionDescription)
                .HasColumnType("text")
                .HasColumnName("permission_description");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(100)
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<VirtualRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("VirtualRole");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.LastModified).HasColumnType("datetime");
            entity.Property(e => e.RoleDescription).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(50);
            entity.Property(e => e.RolePhoto).HasColumnType("image");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
