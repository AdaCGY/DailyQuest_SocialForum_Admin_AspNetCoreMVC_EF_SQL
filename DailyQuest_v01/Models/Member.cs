using System;
using System.Collections.Generic;

namespace DailyQuest_v01.Models;

public partial class Member
{
    public int MemberId { get; set; }

    public string? Username { get; set; }

    public int Gender { get; set; }

    public string? AvatarUrl { get; set; }

    public string? PasswordHash { get; set; }

    public string? PasswordSalt { get; set; }

    public string? LastPasswordHash { get; set; }

    public bool IsThirdPartyAuth { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public decimal Points { get; set; }

    public DateTime RegisterTime { get; set; }

    public int? AddressId { get; set; }

    public int? LevelId { get; set; }

    public int? RoleId { get; set; }

    public int? StatusId { get; set; }

    public virtual City? Address { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CommentsLike> CommentsLikes { get; set; } = new List<CommentsLike>();

    public virtual ICollection<ConversationHistory> ConversationHistoryReceiverMembers { get; set; } = new List<ConversationHistory>();

    public virtual ICollection<ConversationHistory> ConversationHistorySenderMembers { get; set; } = new List<ConversationHistory>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual MemberLevel? Level { get; set; }

    public virtual ICollection<ListOfParticipant> ListOfParticipants { get; set; } = new List<ListOfParticipant>();

    public virtual ICollection<MemberFriend> MemberFriendMemberIdANavigations { get; set; } = new List<MemberFriend>();

    public virtual ICollection<MemberFriend> MemberFriendMemberIdBNavigations { get; set; } = new List<MemberFriend>();

    public virtual ICollection<MemberFriendship> MemberFriendshipMemberIdANavigations { get; set; } = new List<MemberFriendship>();

    public virtual ICollection<MemberFriendship> MemberFriendshipMemberIdBNavigations { get; set; } = new List<MemberFriendship>();

    public virtual ICollection<MemberLoginHistory> MemberLoginHistories { get; set; } = new List<MemberLoginHistory>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<PostsLike> PostsLikes { get; set; } = new List<PostsLike>();

    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<ShopAdmin> ShopAdmins { get; set; } = new List<ShopAdmin>();

    public virtual MemberStatus? Status { get; set; }

    public virtual UserAndTaskR? UserAndTaskR { get; set; }

    public virtual UserAndTitleR? UserAndTitleR { get; set; }
}
