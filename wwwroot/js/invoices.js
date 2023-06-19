window.addEventListener("DOMContentLoaded", Init);

function Init() {
    btn_add_item.addEventListener("click", AddProduct);
}

let item = 0;

function AddProduct() {
    const produc = tb_product.value;
    const qty = tb_qty.value;
    const price = tb_price.value;
    const subTotal = parseInt(qty) * parseFloat(price);
    const totalItbis = Number(subTotal * 0.18).toFixed(2);
    const total = parseFloat(subTotal) + parseFloat(totalItbis);
    const tbody = tb_items.querySelector("tbody");
    
    tbody.insertAdjacentHTML("beforeend", `
        <tr>
            <td>${produc}<input type="hidden" name="InvoiceDetails[${item}].ProductName" value="${produc}" /></td>
            <td>${qty}<input type="hidden" name="InvoiceDetails[${item}].Qty" value="${qty}"  /></td>
            <td>${price}<input type="hidden" name="InvoiceDetails[${item}].Price" value="${price}"  /></td>
            <td>${totalItbis}<input type="hidden" name="InvoiceDetails[${item}].TotalItbis" value="${totalItbis}"  /></td>
            <td>${subTotal}<input type="hidden" name="InvoiceDetails[${item}].SubTotal" value="${subTotal}"  /></td>
            <td>${total}<input type="hidden" name="InvoiceDetails[${item}].Total" value="${total}"  /></td>
        </tr>
    `);

    tb_product.value = "";
    tb_qty.value = 1;
    tb_price.value = 1;
    tb_product.focus();
    item++;
}