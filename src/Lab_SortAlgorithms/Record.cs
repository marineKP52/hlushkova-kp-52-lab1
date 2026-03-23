public class Record
{
    public int reviewId;
    public string movieTitle;
    public string userName;
    public int rating;

    private static int _id = 1;

    public Record(string movieTitle, string userName, int rating)
    {
        reviewId = _id;
        _id++;
        this.movieTitle = movieTitle;
        this.userName = userName;
        this.rating = rating;
    }

    public override string ToString()
    {
        return $"ID: {reviewId} | Movie: {movieTitle} | User: {userName} | Rating: {rating}";
    }
}