import $ from 'jquery';

const filters = {
    categories: [],
    page: 1,
    pageSize: 2,
    sortBy: 'Price',
    sortOrder: 'Ascending'
};

function createItem(item) {
    return `<div class="col">
                    <div class="card text-center">
                        <img src="/img/${item.image}" alt="${item.name}" class="card-img-top img-fluid"/>
                        <div class="card-body">
                            <h3>${item.name}</h3>
                            <h2>
                                <b>${item.price}</b>
                            </h2>
                            <a href="#">Add to cart</a>
                        </div>
                    </div>
                </div>`;
}

$(document).ready(function () {
    getProducts();

    $("#category").on('change', function () {
        filters.categories = $(this).val();
        getProducts();
    });
});

function getProducts() {
    $.ajax({
        url: `/api/v1/products`,
        data: filters,
        traditional: true,
        success: function (data, status, xhr) {
            $("#items").empty().append($.map(data, createItem));
            const count = xhr.getResponseHeader('x-total-count');
            addPaginationButtons(filters.page, count, filters.pageSize);
        }
    });
}

function addPaginationButtons(currentPage, totalCount, pageSize) {
    const pageCount = Math.ceil(totalCount / pageSize);
    const buttons = [];
    for (let i = 1; i <= pageCount; i++) {
        const button = $('<li>', {class: 'page-item'});
        if (i === currentPage) {
            button.addClass('active');
            button.append(`<a class="page-link" href="#">${i} <span class="sr-only">(current)</span></a>`)
        } else {
            button.append(`<a class="page-link" href="#">${i}</a>`)
        }
        buttons.push(button);
    }
    $('.pagination').empty().append(buttons);

    // <li class="page-item"><a class="page-link" href="#">1</a></li>
    // <li class="page-item active" aria-current="page">
    //     <a class="page-link" href="#">2 <span class="sr-only">(current)</span></a>
    // </li>
    // <li class="page-item"><a class="page-link" href="#">3</a></li>
}