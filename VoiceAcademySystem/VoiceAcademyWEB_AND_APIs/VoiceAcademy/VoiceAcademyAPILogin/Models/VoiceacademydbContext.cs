using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VoiceAcademyAPILogin.Models;

public partial class VoiceacademydbContext : DbContext
{
    IConfiguration config = null;
    public VoiceacademydbContext(IConfiguration config)
    {
        this.config = config;
    }

    public VoiceacademydbContext(DbContextOptions<VoiceacademydbContext> options, IConfiguration config)
        : base(options)
    {
        this.config = config;
    }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Degree> Degrees { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Likeslist> Likeslists { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Podcastchannel> Podcastchannels { get; set; }

    public virtual DbSet<Publicationrequest> Publicationrequests { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Uvcomunity> Uvcomunities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = config.GetConnectionString("mysql");
        optionsBuilder.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.IdChapter).HasName("PRIMARY");

            entity.ToTable("chapter");

            entity.HasIndex(e => e.PodcastIdPodcast, "fk_Chapter_Podcast1_idx");

            entity.Property(e => e.IdChapter).HasColumnName("idChapter");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.ImageChapter)
                .HasColumnType("blob")
                .HasColumnName("imageChapter");
            entity.Property(e => e.PodcastIdPodcast).HasColumnName("Podcast_idPodcast");
            entity.Property(e => e.PublicationDate)
                .HasColumnType("datetime")
                .HasColumnName("publicationDate");
            entity.Property(e => e.StateChapter).HasColumnName("stateChapter");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.Topic)
                .HasMaxLength(100)
                .HasColumnName("topic");

            entity.HasOne(d => d.PodcastIdPodcastNavigation).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.PodcastIdPodcast)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Chapter_Podcast1");

            entity.HasMany(d => d.LikesListIdlikeds).WithMany(p => p.ChapterIdChapters)
                .UsingEntity<Dictionary<string, object>>(
                    "ChapterHasLikeslist",
                    r => r.HasOne<Likeslist>().WithMany()
                        .HasForeignKey("LikesListIdliked")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Chapter_has_LikesList_LikesList1"),
                    l => l.HasOne<Chapter>().WithMany()
                        .HasForeignKey("ChapterIdChapter")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Chapter_has_LikesList_Chapter1"),
                    j =>
                    {
                        j.HasKey("ChapterIdChapter", "LikesListIdliked")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("chapter_has_likeslist");
                        j.HasIndex(new[] { "ChapterIdChapter" }, "fk_Chapter_has_LikesList_Chapter1_idx");
                        j.HasIndex(new[] { "LikesListIdliked" }, "fk_Chapter_has_LikesList_LikesList1_idx");
                        j.IndexerProperty<int>("ChapterIdChapter").HasColumnName("Chapter_idChapter");
                        j.IndexerProperty<int>("LikesListIdliked").HasColumnName("LikesList_idliked");
                    });

            entity.HasMany(d => d.PlayListIdPlayLists).WithMany(p => p.ChapterIdChapters)
                .UsingEntity<Dictionary<string, object>>(
                    "ChapterHasPlaylist",
                    r => r.HasOne<Playlist>().WithMany()
                        .HasForeignKey("PlayListIdPlayList")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Chapter_has_PlayList_PlayList1"),
                    l => l.HasOne<Chapter>().WithMany()
                        .HasForeignKey("ChapterIdChapter")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_Chapter_has_PlayList_Chapter1"),
                    j =>
                    {
                        j.HasKey("ChapterIdChapter", "PlayListIdPlayList")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("chapter_has_playlist");
                        j.HasIndex(new[] { "ChapterIdChapter" }, "fk_Chapter_has_PlayList_Chapter1_idx");
                        j.HasIndex(new[] { "PlayListIdPlayList" }, "fk_Chapter_has_PlayList_PlayList1_idx");
                        j.IndexerProperty<int>("ChapterIdChapter").HasColumnName("Chapter_idChapter");
                        j.IndexerProperty<int>("PlayListIdPlayList").HasColumnName("PlayList_idPlayList");
                    });
        });

        modelBuilder.Entity<Degree>(entity =>
        {
            entity.HasKey(e => e.IdDegree).HasName("PRIMARY");

            entity.ToTable("degree");

            entity.HasIndex(e => e.FacultyIdFaculty, "fk_Degree_Faculty1_idx");

            entity.Property(e => e.IdDegree).HasColumnName("idDegree");
            entity.Property(e => e.FacultyIdFaculty).HasColumnName("Faculty_idFaculty");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");

            entity.HasOne(d => d.FacultyIdFacultyNavigation).WithMany(p => p.Degrees)
                .HasForeignKey(d => d.FacultyIdFaculty)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Degree_Faculty1");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.IdFaculty).HasName("PRIMARY");

            entity.ToTable("faculty");

            entity.HasIndex(e => e.RegionIdRegion, "fk_Faculty_Region1_idx");

            entity.Property(e => e.IdFaculty)
                .ValueGeneratedNever()
                .HasColumnName("idFaculty");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.RegionIdRegion).HasColumnName("Region_idRegion");

            entity.HasOne(d => d.RegionIdRegionNavigation).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.RegionIdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Faculty_Region1");
        });

        modelBuilder.Entity<Likeslist>(entity =>
        {
            entity.HasKey(e => e.IdlikesList).HasName("PRIMARY");

            entity.ToTable("likeslist");

            entity.HasIndex(e => e.UserIdUser, "fk_LikesList_User1_idx");

            entity.Property(e => e.IdlikesList).HasColumnName("idlikesList");
            entity.Property(e => e.Followers).HasColumnName("followers");
            entity.Property(e => e.TotalChapters).HasColumnName("totalChapters");
            entity.Property(e => e.UserIdUser).HasColumnName("User_idUser");

            entity.HasOne(d => d.UserIdUserNavigation).WithMany(p => p.Likeslists)
                .HasForeignKey(d => d.UserIdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_LikesList_User1");
        });

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.IdPlayList).HasName("PRIMARY");

            entity.ToTable("playlist");

            entity.HasIndex(e => e.UvcomunityIdUvcomunity, "fk_PlayList_UVComunity1_idx");

            entity.Property(e => e.IdPlayList).HasColumnName("idPlayList");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.Followers).HasColumnName("followers");
            entity.Property(e => e.ImagePlayList).HasColumnName("imagePlayList");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.StateList).HasColumnName("stateList");
            entity.Property(e => e.UvcomunityIdUvcomunity).HasColumnName("UVComunity_idUVComunity");

            entity.HasOne(d => d.UvcomunityIdUvcomunityNavigation).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.UvcomunityIdUvcomunity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PlayList_UVComunity1");
        });

        modelBuilder.Entity<Podcastchannel>(entity =>
        {
            entity.HasKey(e => e.IdPodcast).HasName("PRIMARY");

            entity.ToTable("podcastchannel");

            entity.HasIndex(e => e.UvcomunityIdUvcomunity, "fk_Podcast_UVComunity1_idx");

            entity.Property(e => e.IdPodcast).HasColumnName("idPodcast");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.Description)
                .HasMaxLength(300)
                .HasColumnName("description");
            entity.Property(e => e.ImagePodcast)
                .HasColumnType("blob")
                .HasColumnName("imagePodcast");
            entity.Property(e => e.StatePodcast).HasColumnName("statePodcast");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.Topic)
                .HasMaxLength(100)
                .HasColumnName("topic");
            entity.Property(e => e.UvcomunityIdUvcomunity).HasColumnName("UVComunity_idUVComunity");

            entity.HasOne(d => d.UvcomunityIdUvcomunityNavigation).WithMany(p => p.Podcastchannels)
                .HasForeignKey(d => d.UvcomunityIdUvcomunity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Podcast_UVComunity1");
        });

        modelBuilder.Entity<Publicationrequest>(entity =>
        {
            entity.HasKey(e => e.IdPublicationRequest).HasName("PRIMARY");

            entity.ToTable("publicationrequest");

            entity.HasIndex(e => e.ChapterIdChapter, "fk_PublicationRequest_Chapter1_idx");

            entity.HasIndex(e => e.UvcomunityIdUvcomunity, "fk_PublicationRequest_UVComunity1_idx");

            entity.Property(e => e.IdPublicationRequest).HasColumnName("idPublicationRequest");
            entity.Property(e => e.ChapterIdChapter).HasColumnName("Chapter_idChapter");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.StatusRequest).HasColumnName("statusRequest");
            entity.Property(e => e.UvcomunityIdUvcomunity).HasColumnName("UVComunity_idUVComunity");

            entity.HasOne(d => d.ChapterIdChapterNavigation).WithMany(p => p.Publicationrequests)
                .HasForeignKey(d => d.ChapterIdChapter)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PublicationRequest_Chapter1");

            entity.HasOne(d => d.UvcomunityIdUvcomunityNavigation).WithMany(p => p.Publicationrequests)
                .HasForeignKey(d => d.UvcomunityIdUvcomunity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_PublicationRequest_UVComunity1");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.IdRegion).HasName("PRIMARY");

            entity.ToTable("region");

            entity.Property(e => e.IdRegion).HasColumnName("idRegion");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.IdUser, "idUser_UNIQUE").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("idUser");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.ImageUser)
                .HasColumnType("blob")
                .HasColumnName("imageUser");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("lastName");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Rol)
                .HasMaxLength(45)
                .HasColumnName("rol");
            entity.Property(e => e.StatusUser).HasColumnName("statusUser");

            entity.HasMany(d => d.PodcastChannelIdPodcasts).WithMany(p => p.UserIdUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserHasPodcastchannel",
                    r => r.HasOne<Podcastchannel>().WithMany()
                        .HasForeignKey("PodcastChannelIdPodcast")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_User_has_PodcastChannel_PodcastChannel1"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserIdUser")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_User_has_PodcastChannel_User1"),
                    j =>
                    {
                        j.HasKey("UserIdUser", "PodcastChannelIdPodcast")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("user_has_podcastchannel");
                        j.HasIndex(new[] { "PodcastChannelIdPodcast" }, "fk_User_has_PodcastChannel_PodcastChannel1_idx");
                        j.HasIndex(new[] { "UserIdUser" }, "fk_User_has_PodcastChannel_User1_idx");
                        j.IndexerProperty<int>("UserIdUser").HasColumnName("User_idUser");
                        j.IndexerProperty<int>("PodcastChannelIdPodcast").HasColumnName("PodcastChannel_idPodcast");
                    });
        });

        modelBuilder.Entity<Uvcomunity>(entity =>
        {
            entity.HasKey(e => e.IdUvcomunity).HasName("PRIMARY");

            entity.ToTable("uvcomunity");

            entity.HasIndex(e => e.DegreeIdDegree, "fk_UVComunity_Degree1_idx");

            entity.HasIndex(e => e.UserIdUser, "fk_UVComunity_User_idx").IsUnique();

            entity.HasIndex(e => e.InstitutionalEmail, "institutionalEmail_UNIQUE").IsUnique();

            entity.HasIndex(e => e.StudentNumber, "uvTuition_UNIQUE").IsUnique();

            entity.Property(e => e.IdUvcomunity).HasColumnName("idUVComunity");
            entity.Property(e => e.DegreeIdDegree).HasColumnName("Degree_idDegree");
            entity.Property(e => e.InstitutionalEmail)
                .HasMaxLength(28)
                .HasColumnName("institutionalEmail");
            entity.Property(e => e.StudentNumber)
                .HasMaxLength(10)
                .HasColumnName("studentNumber");
            entity.Property(e => e.UserIdUser).HasColumnName("User_idUser");

            entity.HasOne(d => d.DegreeIdDegreeNavigation).WithMany(p => p.Uvcomunities)
                .HasForeignKey(d => d.DegreeIdDegree)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UVComunity_Degree1");

            entity.HasOne(d => d.UserIdUserNavigation).WithOne(p => p.Uvcomunity)
                .HasForeignKey<Uvcomunity>(d => d.UserIdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_UVComunity_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
