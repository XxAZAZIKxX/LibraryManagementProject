import { deleteBook, getBooks, refresh } from "../modules/api.js";

function stringFormat(str, ...args) {
    return str.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
}

const rowTemplate = `
<tr id="{0}">
    <td>{1}</td>
    <td>{2}</td>
    <td>{3}</td>
    <td>{4}</td>
    <td><a href="book.html?bookId={0}" class="btn btn-outline-primary btn-sm" style="width: 65px;">Edit</button></td>
    <td><button bookId="{0}" class="btn btn-outline-danger btn-sm" style="width: 65px;">Delete</button></td>
</tr>
`

const onDeleteButtonClick = async (ev) => {
    let bookId = ev.target.getAttribute("bookId")
    await deleteBook(bookId)
    document.location.reload()
}

const tryRefreshToken = async () => {
    let [ok, _] = await refresh()
    if (!ok) return false;
    return true;
};

try {
    let [ok, books] = await getBooks();
    if (!ok) {
        let res = await tryRefreshToken()
        if (!res) document.location.href = "../../home.html";
        else document.location.reload()
    }
    books.forEach(book => {
        document.getElementById("items").insertAdjacentHTML('beforeend', stringFormat(rowTemplate,
            book.id,
            book.title,
            `${book.author.name} ${book.author.surname}`,
            book.publish_date,
            book.genre.title
        ))
        let buttons = document.getElementById(book.id).getElementsByTagName("button");
        buttons[0].addEventListener("click", onDeleteButtonClick);
    });
}
catch (e) {
    document.location.href = "../../home.html"
}