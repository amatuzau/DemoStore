// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

async function getProducts(categoryId) {
    const response = await fetch(`/api/v1/categories/${categoryId}/products`, {
        method: "GET",
        headers: { "Accept": "Application/json" }
    })

    if (response.ok === true) {
        const products = await response.json();
        const newBody = document.createElement("tbody")
        products.forEach(p => {
            const tr = document.createElement("tr");

            const tdId = document.createElement("td");
            tdId.append(p.id);

            tr.append(tdId);

            const tdName = document.createElement("td");
            tdName.append(p.name);

            tr.append(tdName);

            const tdPrice = document.createElement("td");
            tdPrice.append(p.price);

            tr.append(tdPrice);

            newBody.append(tr);
        })
        const oldBody = document.querySelector("tbody");
        oldBody.parentNode.replaceChild(newBody, oldBody);
    }
}