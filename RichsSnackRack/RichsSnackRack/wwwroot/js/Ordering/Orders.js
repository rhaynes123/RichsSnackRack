'use strict';
const OrderStatues = {
    Completed: 'Completed',
    Refunded: 'Refunded',
    Cancelled: 'Cancelled'
};
const downloadButton = document.getElementById('downloandOrderDetailReceiptId');
const orderLinkToCopy = document.querySelector("#orderDetailLink");
const searchInput = document.getElementById('searchInput');
const orderItemRows = document.querySelectorAll("tbody tr");
const orderStatuses = document.querySelectorAll('.orderStatus');
const adjustPriceBtn = document.getElementById("adjustPriceBtn");
// TODO the selectors need to have the values passed in so other things can get selected

// Order Confirmation
if (orderLinkToCopy) {
    orderLinkToCopy.addEventListener("click", () => {
        const orderId = document.querySelector("#hiddenOrderDetailId").innerHTML;
        const clipboard = navigator.clipboard;
        clipboard.writeText(orderId);
    });
};

if (downloadButton) {
    downloadButton.addEventListener("click", () => {
        const downloadElement = document.getElementById('orderDetailReceiptId');
        html2pdf().from(downloadElement).save();
    });
};

// All Orders
if (searchInput && orderItemRows) {
    searchInput.addEventListener("keyup", (event) => {
        const searchEvent = event.target.value.toLowerCase();
        orderItemRows.forEach((orderItemRow) => {
            orderItemRow.querySelector("td").textContent.toLocaleLowerCase().startsWith(searchEvent)
                ? (orderItemRow.style.display = "table-row")
                : (orderItemRow.style.display = "none")
        });
    });
};

if (adjustPriceBtn) {
    adjustPriceBtn.addEventListener('click', () => {
        let snackPrice = document.getElementById('Snack_Price');
        snackPrice.removeAttribute('readonly')
    });
};
orderStatuses.forEach(status => {
    switch (status.innerText) {
        case OrderStatues.Completed:
            status.style.color = '#008000'
            break;
        case OrderStatues.Refunded:
            status.style.color = '#FFFF00'
            break;
        case OrderStatues.Cancelled:
            status.style.color = '#FF0000'
            break;
        default:
            break;
    };
});