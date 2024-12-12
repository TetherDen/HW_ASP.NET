using HW_10.Models;

namespace HW_10.Data
{
    public static class DbInitializer
    {
        public static void Init(ApplicationContext context)
        {
            if(!context.Books.Any())
            {
                var books = new[]
                {
                    new Book { Title = "Book1", Author = "Author1", Genre = "Genre1", Price = 100m,
                        Comments = new List<Comment>
                        {
                            new Comment {Text = "First comment" },
                            new Comment {Text = "Secobd comment" },
                            new Comment {Text = "Third comment" },
                        }
                    },

                    new Book { Title = "Book2", Author = "Author2", Genre = "Genre2", Price = 200m,
                        Comments = new List<Comment>
                        {
                            new Comment {Text = "First comment" },
                            new Comment {Text = "Secobd comment" },
                            new Comment {Text = "Third comment" },
                        }
                    },

                    new Book { Title = "Book3", Author = "Author3", Genre = "Genre1", Price = 300m,
                        Comments = new List<Comment>
                        {
                            new Comment {Text = "First comment" },
                            new Comment {Text = "Secobd comment" },
                            new Comment {Text = "Third comment" },
                        }
                    },

                    new Book { Title = "Book4", Author = "Author", Genre = "Genre2", Price = 200m,
                        Comments = new List<Comment>
                        {
                            new Comment {Text = "First comment" },
                            new Comment {Text = "Secobd comment" },
                            new Comment {Text = "Third comment" },
                        }
                    },

                    new Book { Title = "Book5", Author = "Author5", Genre = "Genre1", Price = 300m,
                        Comments = new List<Comment>
                        {
                            new Comment {Text = "First comment" },
                            new Comment {Text = "Secobd comment" },
                            new Comment {Text = "Third comment" },
                        }
                    },
                };

                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}
