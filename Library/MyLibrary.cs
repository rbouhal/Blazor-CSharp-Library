using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

public class MyLibrary
{
    public string[] bookTitles { get; }
    public List<string> checkedOutBooks;

    private int numBooks;

    public MyLibrary()
    {
        bookTitles = new string[5];
        numBooks = 0;
        checkedOutBooks = new List<string>();
    }

    // Compare if two book titles are the same (Ignoring Case Sensitivity)
    public bool CompareBookTitlesIgnoreCase(string title1, string title2)
    {
        return string.Equals(title1, title2, StringComparison.OrdinalIgnoreCase);
    }
    
    // Add new book to list if it's not already in the list (case-insensitive)
    public string AddBook(string title)
    {
        int emptySlotIndex = -1;

        for (int i = 0; i < bookTitles.Length; i++)
        {
            // Check for duplicate (case-insensitive)
            if (!string.IsNullOrWhiteSpace(bookTitles[i]) && CompareBookTitlesIgnoreCase(bookTitles[i], title))
            {
                return $"The book '{title}' is already in the library.";
            }

            // Track the first empty slot
            if (emptySlotIndex == -1 && bookTitles[i] == null)
            {
                emptySlotIndex = i;
            }
        }

        // Add the book if an empty slot is found
        if (emptySlotIndex != -1)
        {
            bookTitles[emptySlotIndex] = title;
            numBooks++;
            return "The number of spaces for books remaining are: " + (5 - numBooks);
        }

        return "There is no more space for books to be added :(";
    }


    // Remove book to list if it's in list
    public string RemoveBook(string title)
    {
        for (int i = 0; i < bookTitles.Length; i++)
        {

            if (CompareBookTitlesIgnoreCase(bookTitles[i], title))
            {
                bookTitles[i] = null;
                numBooks--;
                return "The number of spaces for books remaining are: " + (5 - numBooks);
            }
        }
        return "The book " + title + " does not exist in the library.";
    }

    // Display the books currently in the list
    public void DisplayBooks()
    {
        Console.WriteLine("Book Titles: ");
        int i = 1;
        foreach (string title in bookTitles)
        {
            if (title != null)
            {
                Console.WriteLine(i + ": " + title);
                i++;
            }
        }
    }

    // Search for books in the list
    public void SearchBooks()
    {
        Console.WriteLine("Enter a book title to search for: ");
        string userTitle = Console.ReadLine();
        foreach (string title in bookTitles)
        {
            if (CompareBookTitlesIgnoreCase(title, userTitle))
            {
                Console.WriteLine("The book " + userTitle + " is available!");
                return;
            }
        }
        Console.WriteLine("The book " + userTitle + " is not in the collection.");
    }

    // Check out a book, the limit is 3 books
    public string CheckOutBook(string title)
    {
        for (int i = 0; i < bookTitles.Length; i++)
        {

            if (CompareBookTitlesIgnoreCase(bookTitles[i], title))
            {
                if (checkedOutBooks.Count < 3)
                {
                    bookTitles[i] = null;
                    numBooks--;
                    checkedOutBooks.Add(title);
                    return "You have checked out " + title;
                }
                else
                {
                    return "You have checked out the maximum number of books: 3";
                }
            }
        }
        return "The book " + title + " does not exist in the library.";
    }

    // Check a book back in
    public string CheckInBook(string title)
    {

        foreach (string book in checkedOutBooks)
        {
            if (CompareBookTitlesIgnoreCase(book, title))
            {
                checkedOutBooks.Remove(title);
                AddBook(title);
                return "You have successfully Checked-In " + title;
            }
        }
        return "The book " + title + " was not checked out from the library.";
    }
}
