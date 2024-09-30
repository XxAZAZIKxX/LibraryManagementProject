const endpoint = "https://localhost:7088";

//#region Private

const sha512 = async (message) => {
    // Кодирование строки в формате UTF-8
    const encoder = new TextEncoder();
    const data = encoder.encode(message);

    // Вычисление хэша SHA-512
    const hashBuffer = await crypto.subtle.digest('SHA-512', data);

    // Преобразование результата в массив байтов
    const hashArray = Array.from(new Uint8Array(hashBuffer));

    // Преобразование каждого байта в шестнадцатеричный формат и объединение в строку
    const hashHex = hashArray.map(b => b.toString(16).padStart(2, '0')).join('');

    return hashHex;
}

const addAuthorization = (init) => {
    if (init.headers == undefined) {
        init.headers = {
            "Authorization": `Bearer ${localStorage.getItem("authorization")}`
        };
        return;
    }
    init.headers = Object.assign(init.headers, {
        "Authorization": `Bearer ${localStorage.getItem("authorization")}`
    });
}

const addJsonContentType = (init) => {
    if (init.headers == undefined) {
        init.headers = {
            "Content-Type": "application/json"
        };
        return;
    }
    init.headers = Object.assign(init.headers, {
        "Content-Type": "application/json"
    })
}


const handleResponse = async response => {
    return [response.ok, await response.json()];
}

//#endregion


//#region Auth

export async function login(username, password) {
    let init = {
        method: "POST",
        body: JSON.stringify({
            username,
            password_hash: await sha512(password)
        })
    };
    addJsonContentType(init)
    let response = await fetch(`${endpoint}/api/auth/login`, init);
    return await handleResponse(response);
}

export async function register(username, password) {
    let init = {
        method: "POST",
        body: JSON.stringify({
            username,
            password_hash: await sha512(password)
        })
    };
    addJsonContentType(init);
    let response = await fetch(`${endpoint}/api/auth/register`, init);
    return await handleResponse(response);
}

export async function refresh() {
    let init = {
        method: "POST"
    }
    addAuthorization(init)
    let response = await fetch(`${endpoint}/api/auth/refresh`, init)
    return await handleResponse(response);
}

//#endregion

//#region Books

export async function getBooks() {
    let init = {
        method: "GET"
    }
    addAuthorization(init);
    let response = await fetch(`${endpoint}/api/books`, init)
    return await handleResponse(response);
}

export async function getBook(bookId) {
    let init = {
        method: "GET"
    }
    addAuthorization(init);
    let response = await fetch(`${endpoint}/api/books/${bookId}`, init)
    return await handleResponse(response);
}

export async function addBook(book) {
    let init = {
        method: "POST",
        body: JSON.stringify(book)
    }
    addAuthorization(init);
    addJsonContentType(init);
    let response = await fetch(`${endpoint}/api/books/add`, init)
    return await handleResponse(response)
}

export async function updateBook(bookId, title, authorId, publish_date, genre_id) {
    let body = [];
    if (title) {
        body.push({
            "op": "replace",
            "path": "/title",
            "value": title
        })
    }
    if (authorId) {
        body.push({
            "op": "replace",
            "path": "/authorId",
            "value": authorId
        })
    }
    if (publish_date) {
        body.push({
            "op": "replace",
            "path": "/publish_date",
            "value": publish_date
        })
    }
    if (genre_id) {
        body.push({
            "op": "replace",
            "path": "/genre_id",
            "value": genre_id
        })
    }
    let init = {
        method: "PATCH",
        body: JSON.stringify(body)
    }
    addAuthorization(init);
    addJsonContentType(init);
    let response = await fetch(`${endpoint}/api/books/${bookId}`, init)
    return await handleResponse(response)
}

export async function deleteBook(bookId) {
    let init = {
        method: "DELETE"
    }
    addAuthorization(init)
    let response = await fetch(`${endpoint}/api/books/${bookId}`, init)
    return await handleResponse(response);
}

//#endregion

//#region Authors

export async function getAuthors() {
    let init = {
        method: "GET"
    }
    addAuthorization(init)
    let response = await fetch(`${endpoint}/api/authors`, init)
    return await handleResponse(response);
}

export async function getAuthor(authorId) {
    let init = {
        method: "GET"
    }
    addAuthorization(init)
    let response = await fetch(`${endpoint}/api/authors/${authorId}`, init)
    return await handleResponse(response)
}

export async function addAuthor(author) {
    let init = {
        method: "POST",
        body: JSON.stringify(author)
    }
    addAuthorization(init)
    addJsonContentType(init);
    let response = await fetch(`${endpoint}/api/authors/add`, init)
    return await handleResponse(response)
}

export async function updateAuthor(authorId, name, surname) {
    let body = [];
    if (name) {
        body.push({
            "op": "replace",
            "path": "/name",
            "value": name
        })
    }
    if (surname) {
        body.push({
            "op": "replace",
            "path": "/surname",
            "value": surname
        })
    }
    let init = {
        method: "PATCH",
        body: JSON.stringify(body)
    }
    addAuthorization(init)
    addJsonContentType(init)
    let response = await fetch(`${endpoint}/api/authors/${authorId}`, init)
    return await handleResponse(response)
}

export async function deleteAuthor(authorId) {
    let init = {
        method: "DELETE"
    }
    addAuthorization(init)
    let response = await fetch(`${endpoint}/api/authors/${authorId}`, init)
    return await handleResponse(response)
}

//#endregion

//#region Genres

export async function getGenres() {
    let init = {
        method: "GET"
    }
    addAuthorization(init);
    let response = await fetch(`${endpoint}/api/genres`, init)
    return await handleResponse(response)
}

export async function getGenre(genreId) {
    let init = {
        method: "GET"
    }
    addAuthorization(init)
    let response = await fetch(`${endpoint}/api/genres/${genreId}`, init)
    return await handleResponse(response)
}

export async function addGenre(genre) {
    let init = {
        method: "POST",
        body: JSON.stringify(genre)
    }
    addAuthorization(init)
    addJsonContentType(init)
    let response = await fetch(`${endpoint}/api/genres/add`, init)
    return await handleResponse(response)
}

export async function updateGenre(genreId, title) {
    let body = [];
    if (title) {
        body.push({
            "op": "replace",
            "path": "/title",
            "value": title
        })
    }
    let init = {
        method: "PATCH",
        body: JSON.stringify(body)
    }
    addAuthorization(init)
    addJsonContentType(init)
    let response = await fetch(`${endpoint}/api/genres/${genreId}`, init)
    return await handleResponse(response)
}

export async function deleteGenre(genreId) {
    let init = {
        method: "DELETE"
    }
    addAuthorization(init)
    let response = await fetch(`${endpoint}/api/genres/${genreId}`, init)
    return await handleResponse(response)
}

//#endregion