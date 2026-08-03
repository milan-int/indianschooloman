using Microsoft.EntityFrameworkCore;
using Registration.Domain.Entities;

namespace Registration.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        
        public DbSet<Registration.Domain.Entities.Registration> Registrations { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentExistingSibling> StudentExistingSiblings { get; set; }
        public DbSet<StudentNewApplicantSibling> StudentNewApplicantSiblings { get; set; }
        public DbSet<RegistrationSchoolPreference> RegistrationSchoolPreferences { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ApplicationDetail> ApplicationDetails { get; set; }
        public DbSet<PostalCodeMaster> PostalCodes { get; set; }
        public DbSet<MotherTongueMaster> MotherTongues { get; set; }
        public DbSet<RelationshipMaster> Relationships { get; set; }
        public DbSet<NationalityMaster> Nationalities { get; set; }
        public DbSet<GradeMaster> Grades { get; set; }
        public DbSet<GradeSchoolMaster> GradeSchools { get; set; }
        public DbSet<CountryMaster> Countries { get; set; }
        public DbSet<SiblingSchoolMaster> SiblingSchools { get; set; }
        public DbSet<SiblingClassMaster> SiblingClasses { get; set; }
        public DbSet<GenderMaster> Genders { get; set; }
        public DbSet<PortalLinkMaster> PortalLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Registration -> Student (1 to 1)
            modelBuilder.Entity<Registration.Domain.Entities.Registration>()
                .HasOne(r => r.Student)
                .WithOne(s => s.Registration)
                .HasForeignKey<Student>(s => s.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Registration -> Parent (1 to 1)
            modelBuilder.Entity<Registration.Domain.Entities.Registration>()
                .HasOne(r => r.Parent)
                .WithOne(p => p.Registration)
                .HasForeignKey<Parent>(p => p.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Registration -> Address (1 to 1)
            modelBuilder.Entity<Registration.Domain.Entities.Registration>()
                .HasOne(r => r.Address)
                .WithOne(a => a.Registration)
                .HasForeignKey<Address>(a => a.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Registration -> ApplicationDetail (1 to 1)
            modelBuilder.Entity<Registration.Domain.Entities.Registration>()
                .HasOne(r => r.ApplicationDetail)
                .WithOne(ad => ad.Registration)
                .HasForeignKey<ApplicationDetail>(ad => ad.RegistrationId)
                .OnDelete(DeleteBehavior.Cascade);
                
                        // Student -> ExistingSiblings (1 to Many)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.ExistingSiblings)
                .WithOne(es => es.Student)
                .HasForeignKey(es => es.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Student -> NewApplicantSiblings (1 to Many)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.NewApplicantSiblings)
                .WithOne(ns => ns.Student)
                .HasForeignKey(ns => ns.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure unique Registration No
            modelBuilder.Entity<Registration.Domain.Entities.Registration>()
                .HasIndex(r => r.RegistrationNo)
                .IsUnique();
                
            // Seed Master Data
            modelBuilder.Entity<PostalCodeMaster>().HasData(
                new PostalCodeMaster { Id = 1, Code = "Muscat-100", Name = "Muscat" },
                new PostalCodeMaster { Id = 2, Code = "Bareeq Al Shatti-103", Name = "Bareeq Al Shatti" },
                new PostalCodeMaster { Id = 3, Code = "Markaz al Bahja-104", Name = "Markaz al Bahja" },
                new PostalCodeMaster { Id = 4, Code = "Central Post Office-111", Name = "Central Post Office" },
                new PostalCodeMaster { Id = 5, Code = "Ruwi-112", Name = "Ruwi" },
                new PostalCodeMaster { Id = 6, Code = "Muscat-113", Name = "Muscat" },
                new PostalCodeMaster { Id = 7, Code = "Jabroo-114", Name = "Jabroo" },
                new PostalCodeMaster { Id = 8, Code = "Madinat Al Sultan Qaboos-115", Name = "Madinat Al Sultan Qaboos" },
                new PostalCodeMaster { Id = 9, Code = "Mina Al Fahal-116", Name = "Mina Al Fahal" },
                new PostalCodeMaster { Id = 10, Code = "Al Wadi Al Kabir-117", Name = "Al Wadi Al Kabir" },
                new PostalCodeMaster { Id = 11, Code = "Al Harthy Complex-118", Name = "Al Harthy Complex" },
                new PostalCodeMaster { Id = 12, Code = "Al Amarat-119", Name = "Al Amarat" },
                new PostalCodeMaster { Id = 13, Code = "Qurayat-120", Name = "Qurayat" },
                new PostalCodeMaster { Id = 14, Code = "Al Seeb-121", Name = "Al Seeb" },
                new PostalCodeMaster { Id = 15, Code = "Al-Maabela -122", Name = "Al-Maabela " },
                new PostalCodeMaster { Id = 16, Code = "Sultan Qaboos University-123", Name = "Sultan Qaboos University" },
                new PostalCodeMaster { Id = 17, Code = "Al Rusayl -124", Name = "Al Rusayl " },
                new PostalCodeMaster { Id = 18, Code = "Muttrah-125", Name = "Muttrah" },
                new PostalCodeMaster { Id = 19, Code = "Oman Commercial Center-126", Name = "Oman Commercial Center" },
                new PostalCodeMaster { Id = 20, Code = "Khaula Hospital-127", Name = "Khaula Hospital" },
                new PostalCodeMaster { Id = 21, Code = "Seeb Airport-128", Name = "Seeb Airport" },
                new PostalCodeMaster { Id = 22, Code = "Al Murtafa-129", Name = "Al Murtafa" },
                new PostalCodeMaster { Id = 23, Code = "Azaiba-130", Name = "Azaiba" },
                new PostalCodeMaster { Id = 24, Code = "Al Hamriyah-131", Name = "Al Hamriyah" },
                new PostalCodeMaster { Id = 25, Code = "Al Khud-132", Name = "Al Khud" },
                new PostalCodeMaster { Id = 26, Code = "Al Khuwair-133", Name = "Al Khuwair" },
                new PostalCodeMaster { Id = 27, Code = "Jawharat Al Shati-134", Name = "Jawharat Al Shati" },
                new PostalCodeMaster { Id = 28, Code = "Central Salalah -211", Name = "Central Salalah " },
                new PostalCodeMaster { Id = 29, Code = "Quirun Hairiti-212", Name = "Quirun Hairiti" },
                new PostalCodeMaster { Id = 30, Code = "Teetam-213", Name = "Teetam" },
                new PostalCodeMaster { Id = 31, Code = "Al-Dahareez-214", Name = "Al-Dahareez" },
                new PostalCodeMaster { Id = 32, Code = "Al-Hafa-216", Name = "Al-Hafa" },
                new PostalCodeMaster { Id = 33, Code = "AlAwqadain-217", Name = "AlAwqadain" },
                new PostalCodeMaster { Id = 34, Code = "Taqa-218", Name = "Taqa" },
                new PostalCodeMaster { Id = 35, Code = "Madinat-Al - Haq-219", Name = "Madinat-Al - Haq" },
                new PostalCodeMaster { Id = 36, Code = "Marbat-220", Name = "Marbat" },
                new PostalCodeMaster { Id = 37, Code = "Tiwi Atair-221", Name = "Tiwi Atair" },
                new PostalCodeMaster { Id = 38, Code = "Thamrait-222", Name = "Thamrait" },
                new PostalCodeMaster { Id = 39, Code = "Sohar-311", Name = "Sohar" },
                new PostalCodeMaster { Id = 40, Code = "Al Musana a -312", Name = "Al Musana a " },
                new PostalCodeMaster { Id = 41, Code = "Widam-Al - Sahil-313", Name = "Widam-Al - Sahil" },
                new PostalCodeMaster { Id = 42, Code = "Al-Malda -314", Name = "Al-Malda " },
                new PostalCodeMaster { Id = 43, Code = "Al Suwaiq-315", Name = "Al Suwaiq" },
                new PostalCodeMaster { Id = 44, Code = "Al Bidaya-316", Name = "Al Bidaya" },
                new PostalCodeMaster { Id = 45, Code = "Al Awabi-317", Name = "Al Awabi" },
                new PostalCodeMaster { Id = 46, Code = "Al Rustaq-318", Name = "Al Rustaq" },
                new PostalCodeMaster { Id = 47, Code = "Saham-319", Name = "Saham" },
                new PostalCodeMaster { Id = 48, Code = "Barka-320", Name = "Barka" },
                new PostalCodeMaster { Id = 49, Code = "Al Tarif-321", Name = "Al Tarif" },
                new PostalCodeMaster { Id = 50, Code = "Falaj-Al - Qabail-322", Name = "Falaj-Al - Qabail" },
                new PostalCodeMaster { Id = 51, Code = "Nakhal-323", Name = "Nakhal" },
                new PostalCodeMaster { Id = 52, Code = "Shinas-324", Name = "Shinas" },
                new PostalCodeMaster { Id = 53, Code = "Liwa-325", Name = "Liwa" },
                new PostalCodeMaster { Id = 54, Code = "Al Khaboura-326", Name = "Al Khaboura" },
                new PostalCodeMaster { Id = 55, Code = "Sohar-Industrial-327", Name = "Sohar-Industrial" },
                new PostalCodeMaster { Id = 56, Code = "Burj-Al - Radah-329", Name = "Burj-Al - Radah" },
                new PostalCodeMaster { Id = 57, Code = "Sur-411", Name = "Sur" },
                new PostalCodeMaster { Id = 58, Code = "Al-Kamil & Al-Wafi -412", Name = "Al-Kamil & Al-Wafi " },
                new PostalCodeMaster { Id = 59, Code = "Ibra-413", Name = "Ibra" },
                new PostalCodeMaster { Id = 60, Code = "Masirah-414", Name = "Masirah" },
                new PostalCodeMaster { Id = 61, Code = "Ja alan Bani Bu Hassan-415", Name = "Ja alan Bani Bu Hassan" },
                new PostalCodeMaster { Id = 62, Code = "Ja alan Bani Bu Ali -416", Name = "Ja alan Bani Bu Ali " },
                new PostalCodeMaster { Id = 63, Code = "Wadi Bani Khalid -417", Name = "Wadi Bani Khalid " },
                new PostalCodeMaster { Id = 64, Code = "Sinaw-418", Name = "Sinaw" },
                new PostalCodeMaster { Id = 65, Code = "Al Mudhairib-419", Name = "Al Mudhairib" },
                new PostalCodeMaster { Id = 66, Code = "Al Mudhaibi-420", Name = "Al Mudhaibi" },
                new PostalCodeMaster { Id = 67, Code = "Bidiyah-421", Name = "Bidiyah" },
                new PostalCodeMaster { Id = 68, Code = "Al-Ashkharah-422", Name = "Al-Ashkharah" },
                new PostalCodeMaster { Id = 69, Code = "Samad-Al - Shan-423", Name = "Samad-Al - Shan" },
                new PostalCodeMaster { Id = 70, Code = "Dama-Wattaeen-424", Name = "Dama-Wattaeen" },
                new PostalCodeMaster { Id = 71, Code = "Sur Commercial District -425", Name = "Sur Commercial District " },
                new PostalCodeMaster { Id = 72, Code = "Ibri-511", Name = "Ibri" },
                new PostalCodeMaster { Id = 73, Code = "Al-Buraimi -512", Name = "Al-Buraimi " },
                new PostalCodeMaster { Id = 74, Code = "Yanqul-513", Name = "Yanqul" },
                new PostalCodeMaster { Id = 75, Code = "Dhank-514", Name = "Dhank" },
                new PostalCodeMaster { Id = 76, Code = "Al-Araqi -515", Name = "Al-Araqi " },
                new PostalCodeMaster { Id = 77, Code = "Al-Akhdar -Ibri 516", Name = "Al-Akhdar -Ibri 516" },
                new PostalCodeMaster { Id = 78, Code = "Al-Sinainah -517", Name = "Al-Sinainah " },
                new PostalCodeMaster { Id = 79, Code = "Mahda-518", Name = "Mahda" },
                new PostalCodeMaster { Id = 80, Code = "Khasab-811", Name = "Khasab" },
                new PostalCodeMaster { Id = 81, Code = "Bakha-812", Name = "Bakha" },
                new PostalCodeMaster { Id = 82, Code = "Daba-813", Name = "Daba" },
                new PostalCodeMaster { Id = 83, Code = "Madha-814", Name = "Madha" },
                new PostalCodeMaster { Id = 84, Code = "Nizwa-611", Name = "Nizwa" },
                new PostalCodeMaster { Id = 85, Code = "Bahla-612", Name = "Bahla" },
                new PostalCodeMaster { Id = 86, Code = "Bidbid-613", Name = "Bidbid" },
                new PostalCodeMaster { Id = 87, Code = "Izki-614", Name = "Izki" },
                new PostalCodeMaster { Id = 88, Code = "Lizgh-615", Name = "Lizgh" },
                new PostalCodeMaster { Id = 89, Code = "Birkat-616", Name = "Birkat" },
                new PostalCodeMaster { Id = 90, Code = "Al-Hamra-617", Name = "Al-Hamra" },
                new PostalCodeMaster { Id = 91, Code = "Adam-618", Name = "Adam" },
                new PostalCodeMaster { Id = 92, Code = "Manah-619", Name = "Manah" },
                new PostalCodeMaster { Id = 93, Code = "Samail-620", Name = "Samail" },
                new PostalCodeMaster { Id = 94, Code = "Jabal Al-Akhdar-621", Name = "Jabal Al-Akhdar" },
                new PostalCodeMaster { Id = 95, Code = "Haima-711", Name = "Haima" },
                new PostalCodeMaster { Id = 96, Code = "Al Jazr-712", Name = "Al Jazr" }
            );

            modelBuilder.Entity<GenderMaster>().HasData(
                new GenderMaster { Id = 1, Name = "Male", DisplayOrder = 1 },
                new GenderMaster { Id = 2, Name = "Female", DisplayOrder = 2 }
            );

            modelBuilder.Entity<MotherTongueMaster>().HasData(
                new MotherTongueMaster { Id = 1, Name = "Bengali", DisplayOrder = 1 },
                new MotherTongueMaster { Id = 2, Name = "Gujarati", DisplayOrder = 2 },
                new MotherTongueMaster { Id = 3, Name = "Hindi", DisplayOrder = 3 },
                new MotherTongueMaster { Id = 4, Name = "Kannada", DisplayOrder = 4 },
                new MotherTongueMaster { Id = 5, Name = "Malayalam", DisplayOrder = 5 },
                new MotherTongueMaster { Id = 6, Name = "Marathi", DisplayOrder = 6 },
                new MotherTongueMaster { Id = 7, Name = "Punjabi", DisplayOrder = 7 },
                new MotherTongueMaster { Id = 8, Name = "Tamil", DisplayOrder = 8 },
                new MotherTongueMaster { Id = 9, Name = "Telugu", DisplayOrder = 9 },
                new MotherTongueMaster { Id = 10, Name = "Urdu", DisplayOrder = 10 },
                new MotherTongueMaster { Id = 11, Name = "Others", DisplayOrder = 11 }
            );

            modelBuilder.Entity<RelationshipMaster>().HasData(
                new RelationshipMaster { Id = 1, Name = "Father", DisplayOrder = 1 },
                new RelationshipMaster { Id = 2, Name = "Mother", DisplayOrder = 2 },
                new RelationshipMaster { Id = 3, Name = "GrandFather", DisplayOrder = 3 },
                new RelationshipMaster { Id = 4, Name = "GrandMother", DisplayOrder = 4 },
                new RelationshipMaster { Id = 5, Name = "Brother/Sister", DisplayOrder = 5 },
                new RelationshipMaster { Id = 6, Name = "Twins", DisplayOrder = 6 },
                new RelationshipMaster { Id = 7, Name = "Triplets", DisplayOrder = 7 }
            );

            modelBuilder.Entity<NationalityMaster>().HasData(
                new NationalityMaster { Id = 1, Name = "India", DisplayOrder = 1 },
                new NationalityMaster { Id = 2, Name = "Oman", DisplayOrder = 2 },
                new NationalityMaster { Id = 3, Name = "Others", DisplayOrder = 3 }
            );

            // Configure Grades unique index
            modelBuilder.Entity<GradeMaster>()
                .HasIndex(g => g.GradeCode)
                .IsUnique();

            // Configure GradeSchools unique constraint and foreign key
            modelBuilder.Entity<GradeSchoolMaster>()
                .HasIndex(gs => new { gs.GradeId, gs.SchoolName })
                .IsUnique()
                .HasDatabaseName("unique_grade_school");

            modelBuilder.Entity<GradeSchoolMaster>()
                .HasOne(gs => gs.Grade)
                .WithMany(g => g.GradeSchools)
                .HasForeignKey(gs => gs.GradeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Master Grades
            modelBuilder.Entity<GradeMaster>().HasData(
                new GradeMaster { Id = 1, GradeCode = "LKG_1", GradeDisplay = "LKG 1", PreferenceCount = 8 },
                new GradeMaster { Id = 2, GradeCode = "LKG_2", GradeDisplay = "LKG 2", PreferenceCount = 8 },
                new GradeMaster { Id = 3, GradeCode = "LKG_3", GradeDisplay = "LKG 3", PreferenceCount = 9 },
                new GradeMaster { Id = 4, GradeCode = "STD_I", GradeDisplay = "Std I", PreferenceCount = 11 },
                new GradeMaster { Id = 5, GradeCode = "STD_II", GradeDisplay = "Std II", PreferenceCount = 10 },
                new GradeMaster { Id = 6, GradeCode = "STD_III", GradeDisplay = "Std III", PreferenceCount = 8 },
                new GradeMaster { Id = 7, GradeCode = "STD_IV", GradeDisplay = "Std IV", PreferenceCount = 8 },
                new GradeMaster { Id = 8, GradeCode = "STD_V", GradeDisplay = "Std V", PreferenceCount = 7 },
                new GradeMaster { Id = 9, GradeCode = "STD_VI", GradeDisplay = "Std VI", PreferenceCount = 7 },
                new GradeMaster { Id = 10, GradeCode = "STD_VII", GradeDisplay = "Std VII", PreferenceCount = 7 },
                new GradeMaster { Id = 11, GradeCode = "STD_VIII", GradeDisplay = "Std VIII", PreferenceCount = 6 },
                new GradeMaster { Id = 12, GradeCode = "STD_IX", GradeDisplay = "Std IX", PreferenceCount = 5 },
                new GradeMaster { Id = 13, GradeCode = "STD_XI", GradeDisplay = "Std XI", PreferenceCount = 5 }
            );

            // Configure Countries unique index
            modelBuilder.Entity<CountryMaster>()
                .HasIndex(c => c.Name)
                .IsUnique();
            modelBuilder.Entity<CountryMaster>()
                .HasIndex(c => c.Code)
                .IsUnique();

            // Seed Master Countries
            modelBuilder.Entity<CountryMaster>().HasData(
                new CountryMaster { Id = 1, Name = "Australia", Code = "AU" },
                new CountryMaster { Id = 2, Name = "Bangladesh", Code = "BD" },
                new CountryMaster { Id = 3, Name = "Canada", Code = "CA" },
                new CountryMaster { Id = 4, Name = "India", Code = "IN" },
                new CountryMaster { Id = 5, Name = "Malaysia", Code = "MY" },
                new CountryMaster { Id = 6, Name = "Maldives", Code = "MV" },
                new CountryMaster { Id = 7, Name = "Nepal", Code = "NP" },
                new CountryMaster { Id = 8, Name = "New Zealand", Code = "NZ" },
                new CountryMaster { Id = 9, Name = "Oman", Code = "OM" },
                new CountryMaster { Id = 10, Name = "Pakistan", Code = "PK" },
                new CountryMaster { Id = 11, Name = "Qatar", Code = "QA" },
                new CountryMaster { Id = 12, Name = "Saudi Arabia", Code = "SA" },
                new CountryMaster { Id = 13, Name = "Singapore", Code = "SG" },
                new CountryMaster { Id = 14, Name = "South Africa", Code = "ZA" },
                new CountryMaster { Id = 15, Name = "Sri Lanka", Code = "LK" },
                new CountryMaster { Id = 16, Name = "Swaziland", Code = "SZ" },
                new CountryMaster { Id = 17, Name = "United Arab Emirates", Code = "AE" },
                new CountryMaster { Id = 18, Name = "United Kingdom", Code = "GB" },
                new CountryMaster { Id = 19, Name = "United States", Code = "US" },
                new CountryMaster { Id = 20, Name = "Zimbabwe", Code = "ZW" },
                new CountryMaster { Id = 21, Name = "Others", Code = "OTH" }
            );

            // Seed Sibling Schools
            modelBuilder.Entity<SiblingSchoolMaster>().HasData(
                new SiblingSchoolMaster { Id = 1, Name = "Indian School Bousher", DisplayOrder = 1 },
                new SiblingSchoolMaster { Id = 2, Name = "Indian School Muscat", DisplayOrder = 2 },
                new SiblingSchoolMaster { Id = 3, Name = "Indian School Darsait", DisplayOrder = 3 },
                new SiblingSchoolMaster { Id = 4, Name = "Indian School Al Wadi Al Kabir", DisplayOrder = 4 },
                new SiblingSchoolMaster { Id = 5, Name = "Indian School Al Ghubra", DisplayOrder = 5 },
                new SiblingSchoolMaster { Id = 6, Name = "Indian School Al Seeb", DisplayOrder = 6 },
                new SiblingSchoolMaster { Id = 7, Name = "Indian School Al Maabela", DisplayOrder = 7 },
                new SiblingSchoolMaster { Id = 8, Name = "Indian School Al Wadi Al Kabir (International)", DisplayOrder = 8 },
                new SiblingSchoolMaster { Id = 9, Name = "Indian School Al Ghubra (International)", DisplayOrder = 9 },
                new SiblingSchoolMaster { Id = 10, Name = "Indian School Muscat - Afternoonshift", DisplayOrder = 10 },
                new SiblingSchoolMaster { Id = 11, Name = "Indian School Darsait - Afternoonshift", DisplayOrder = 11 }
            );

            // Seed Sibling Classes
            modelBuilder.Entity<SiblingClassMaster>().HasData(
                new SiblingClassMaster { Id = 1, Name = "KG I", DisplayOrder = 1 },
                new SiblingClassMaster { Id = 2, Name = "KG II", DisplayOrder = 2 },
                new SiblingClassMaster { Id = 3, Name = "Std I", DisplayOrder = 3 },
                new SiblingClassMaster { Id = 4, Name = "Std II", DisplayOrder = 4 },
                new SiblingClassMaster { Id = 5, Name = "Std III", DisplayOrder = 5 },
                new SiblingClassMaster { Id = 6, Name = "Std IV", DisplayOrder = 6 },
                new SiblingClassMaster { Id = 7, Name = "Std V", DisplayOrder = 7 },
                new SiblingClassMaster { Id = 8, Name = "Std VI", DisplayOrder = 8 },
                new SiblingClassMaster { Id = 9, Name = "Std VII", DisplayOrder = 9 },
                new SiblingClassMaster { Id = 10, Name = "Std VIII", DisplayOrder = 10 },
                new SiblingClassMaster { Id = 11, Name = "Std IX", DisplayOrder = 11 },
                new SiblingClassMaster { Id = 12, Name = "Std X", DisplayOrder = 12 },
                new SiblingClassMaster { Id = 13, Name = "Std XI", DisplayOrder = 13 },
                new SiblingClassMaster { Id = 14, Name = "Std XII", DisplayOrder = 14 }
            );

            // Seed Portal Links (Admission links and Footer links)
            var seedDate = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            modelBuilder.Entity<PortalLinkMaster>().HasData(
                new PortalLinkMaster { Id = 1, Title = "NEW APPLICATION", Section = "ADMISSION_LINK", LinkType = "INTERNAL_ROUTE", TargetUrl = "/register", Description = "Register a new student for Academic Year 2026–2027", DisplayOrder = 1, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 2, Title = "Notice to Parents", Section = "ADMISSION_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/notice_to_parents.pdf", Description = "Important announcements and eligibility criteria", DisplayOrder = 2, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 3, Title = "Indian Schools Websites", Section = "ADMISSION_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/indian_schools_websites.pdf", Description = "Directory & links of all Indian Schools in Oman", DisplayOrder = 3, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 4, Title = "FAQ", Section = "ADMISSION_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/faq.pdf", Description = "Find answers regarding admission procedures", DisplayOrder = 4, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 5, Title = "Languages offered in Schools", Section = "ADMISSION_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/languages_offered.pdf", Description = "Overview of 2nd & 3rd languages available per school", DisplayOrder = 5, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 6, Title = "Inter-School Transfer", Section = "ADMISSION_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/inter_school_transfer.pdf", Description = "Transfer guidelines between Indian schools in Oman", DisplayOrder = 6, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 7, Title = "Admissions to Other Nationalities", Section = "ADMISSION_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/admissions_other_nationalities.pdf", Description = "Registration guidelines for non-Indian passport holders", DisplayOrder = 7, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 8, Title = "Projected Vacancies", Section = "ADMISSION_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/projected_vacancies.pdf", Description = "Check seat availability across all classes & schools", DisplayOrder = 8, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 9, Title = "Product Description", Section = "FOOTER_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/annexure_a.pdf", Description = "Portal product description", DisplayOrder = 1, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 10, Title = "Privacy Policy", Section = "FOOTER_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/notice_to_parents.pdf", Description = "Privacy policy details", DisplayOrder = 2, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 11, Title = "Delivery Policy", Section = "FOOTER_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/annexure_a.pdf", Description = "Delivery terms and policy", DisplayOrder = 3, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate },
                new PortalLinkMaster { Id = 12, Title = "ContactUS", Section = "FOOTER_LINK", LinkType = "PDF_DOCUMENT", TargetUrl = "assets/docs/indian_schools_websites.pdf", Description = "Contact details and helpdesk information", DisplayOrder = 4, IsActive = true, OpenInNewTab = false, CreatedAt = seedDate }
            );
        }
    }
}



