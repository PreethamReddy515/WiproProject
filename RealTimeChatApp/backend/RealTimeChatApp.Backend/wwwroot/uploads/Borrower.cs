using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book
{
   
        public class Borrower
        {
            public string Name { get; set; }
            public string LibraryCardNumber { get; set; }
            public List<Book1> BorrowedBooks { get; private set; }

            public Borrower()
            {
                BorrowedBooks = new List<Book1>();
            }

            public void BorrowBook(Book1 book)
            {
                if (!book.IsBorrowed)
                {
                    book.Borrow();
                    BorrowedBooks.Add(book);
                }
            }

            public void ReturnBook(Book1 book)
            {
                if (BorrowedBooks.Contains(book))
                {
                    book.Return();
                    BorrowedBooks.Remove(book);
                }
            }
        }
}
