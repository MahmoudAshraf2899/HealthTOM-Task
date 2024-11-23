namespace Boilerplate.Shared.Helpers;

public enum PhotosDirectory
{
    Journals = 1,
    News = 2,
    PhotoMedia = 3,
    PhotoAlbum = 4,
    Event = 5,
    TrainingProgram = 6,
    Users = 7,
    ServicesAndTools = 8,
    IconsLibrary = 9,
    BoilerplateHead = 10,
    BoilerplateHistory = 11,
    LawsRegulations = 12,
    OrganizationStructure = 13,
    EgyptStatisticsJournal = 14,
    Migration = 15,
    Publication = 16,
    PublicationNews = 17,
    HappeningNow = 18

}

public class PhotoDirectories
{
    public PhotosDirectory Directory { get; set; }

    private static readonly Dictionary<int, string> PhotosDirectory = new Dictionary<int, string>
    {
        { 1, "Journals" },
        { 2, "News" },
        { 3, "PhotoMedia" },
        { 4, "PhotoAlbum" },
        { 5, "Event" },
        { 6, "TrainingProgram" },
        { 7, "Users" },
        { 8, "ServicesAndTools" },
        { 9, "IconsLibrary" },
        { 10, "BoilerplateHead" },
        { 11, "BoilerplateHistory" },
        { 12, "LawsRegulations" },
        { 13, "OrganizationStructure" },
        { 14, "EgyptStatisticsJournal" },
        { 15, "Migration" },
        { 16, "Publication" },
        { 17, "PublicationNews" },
        { 18, "HappeningNow" }

    };

    public static string GetPhotoDirectory(int key)
    {
        return PhotosDirectory[key];
    }
}