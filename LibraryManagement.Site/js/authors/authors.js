import { deleteAuthor, getAuthors } from "../modules/api.js";

function stringFormat(str, ...args) {
    return str.replace(/{(\d+)}/g, function (match, number) {
        return typeof args[number] != 'undefined' ? args[number] : match;
    });
}

const rowTemplate = `
<tr id="{0}">
    <td>{0}</td>
    <td>{1}</td>
    <td>{2}</td>
    <td><a href="author.html?authorId={0}" class="btn btn-outline-primary btn-sm" style="width: 65px;">Edit</button></td>
    <td><button authorId="{0}" class="btn btn-outline-danger btn-sm" style="width: 65px;">Delete</button></td>
</tr>
`

const onDeleteButtonClick = async (ev) => {
    let authorId = ev.target.getAttribute("authorId")
    await deleteAuthor(authorId)
    document.location.reload()
}

const tryRefreshToken = async () => {
    let [ok, _] = await refresh()
    if (!ok) return false;
    return true;
};

try {
    let [ok, authors] = await getAuthors();
    if (!ok) {
        let res = await tryRefreshToken()
        if (!res) document.location.href = "../../home.html";
        else document.location.reload()
    }
    authors.forEach(author => {
        document.getElementById("items").insertAdjacentHTML('beforeend', stringFormat(rowTemplate,
            author.id,
            author.name,
            author.surname
        ))
        let buttons = document.getElementById(author.id).getElementsByTagName("button");
        buttons[0].addEventListener("click", onDeleteButtonClick);
    });
}
catch (e) {
    document.location.href = "../../home.html"
}