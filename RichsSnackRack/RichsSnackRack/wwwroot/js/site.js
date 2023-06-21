// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



const downloadButton = document.getElementById('downloandOrderDetailReceiptId');

// TODO the selectors need to have the values passed in so other things can get selected
document.querySelector("#copyBtnId").addEventListener("click", () => {
    let orderId = document.querySelector("#orderDetailId").innerHTML;
    let clipboard = navigator.clipboard;
    clipboard.writeText(orderId);
})



// Named functions

function downloadReceiptPDF() {
    // https://pspdfkit.com/blog/2019/html-to-pdf-in-javascript/
    const downloadElement = document.getElementById('orderDetailReceiptId');
    html2pdf().from(downloadElement).save();
}

downloadButton.addEventListener("click", downloadReceiptPDF)