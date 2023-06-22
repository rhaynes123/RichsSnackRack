// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



const downloadButton = document.getElementById('downloandOrderDetailReceiptId');
const orderLinkToCopy = document.querySelector("#orderDetailLink");
const searchInput = document.getElementById('searchInput');
const orderItemRows = document.querySelectorAll("tbody tr");

// TODO the selectors need to have the values passed in so other things can get selected
// TODO consider moving these4 functions to their own script files or folders

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
}