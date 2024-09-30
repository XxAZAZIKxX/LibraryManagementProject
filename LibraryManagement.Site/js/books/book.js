import { addBook, getBook, updateBook } from "../modules/api.js";

const urlParams = new URLSearchParams(window.location.search);
const bookId = urlParams.get("bookId");

const guidRegExp = /^[{(]?[0-9A-F]{8}[-]?(?:[0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$/i;

(async function () {
    if (!bookId) return;
    try {
        let [ok, response] = await getBook(bookId)
        if (!ok) throw new Error("Unauthorized");

        document.getElementById("form_title").innerText = `Book "${response.title}"`;
        document.getElementById("title").value = response.title;
        document.getElementById("author_id").value = response.author_id;
        document.getElementById("publish_date").value = response.publish_date;
        document.getElementById("genre_id").value = response.genre_id;
    }
    catch {
        window.location.href = "../../books/books.html"
    }
}());

document.getElementById("book_form").addEventListener("submit", async ev => {
    ev.preventDefault();
    let title = ev.target["title"].value;
    let author_id = ev.target["author_id"].value;
    let publish_date = ev.target["publish_date"].value;
    let genre_id = ev.target["genre_id"].value;

    let invalid = false

    if (!guidRegExp.test(author_id)) {
        let element = document.getElementById("author_id");
        element.classList.add("is-invalid");
        element.parentElement.classList.add("is-invalid");
        invalid = true;
    }
    if (!guidRegExp.test(genre_id)) {
        let element = document.getElementById("genre_id");
        element.classList.add("is-invalid");
        element.parentElement.classList.add("is-invalid");
        invalid = true;
    }

    if (invalid) return

    if (!bookId) await addBook({ title, author_id, publish_date, genre_id })
    else await updateBook(bookId, title, author_id, publish_date, genre_id)
    window.location.href = "../../books/books.html"
});