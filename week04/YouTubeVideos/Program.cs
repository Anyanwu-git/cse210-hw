using System;
using System.Collections.Generic;

namespace Foundation1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a list to hold all the videos
            List<Video> videos = new List<Video>();

            // ----- Video 1 -----
            Video video1 = new Video("How to Braid Knotless Box Braids",
                                     "Ollys Beauty",
                                     630); // 10:30

            video1.AddComment(new Comment("Sarah", "I learned so much, thank you!"));
            video1.AddComment(new Comment("Daniel", "Your sectioning is so clean."));
            video1.AddComment(new Comment("Mimi", "Please do a video on boho braids next."));
            video1.AddComment(new Comment("James", "Subbed! Love your teaching style."));

            videos.Add(video1);

            // ----- Video 2 -----
            Video video2 = new Video("C# Classes Explained in 15 Minutes",
                                     "CodeSmart",
                                     900);

            video2.AddComment(new Comment("Lilian", "This helped me understand my CSE 210 homework."));
            video2.AddComment(new Comment("Alex", "Clear explanation, thanks!"));
            video2.AddComment(new Comment("Rita", "Please make one on inheritance."));

            videos.Add(video2);

            // ----- Video 3 -----
            Video video3 = new Video("Morning Study Routine for Programmers",
                                     "StudyWithMe",
                                     780);

            video3.AddComment(new Comment("Chuks", "This motivated me to wake up earlier."));
            video3.AddComment(new Comment("Bella", "Loved the lo-fi music playlist."));
            video3.AddComment(new Comment("John", "Can you share your Notion template?"));

            videos.Add(video3);

            // ----- Video 4 (optional extra) -----
            Video video4 = new Video("Top 5 VS Code Extensions for C#",
                                     "DevTips",
                                     520);

            video4.AddComment(new Comment("Sam", "Number 3 changed my life."));
            video4.AddComment(new Comment("Priya", "I didn’t know about the C# extensions, thanks!"));
            video4.AddComment(new Comment("Kevin", "Please do one for web dev next."));

            videos.Add(video4);

            // ----- Display all video info -----
            foreach (Video video in videos)
            {
                video.DisplayDetails();
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
