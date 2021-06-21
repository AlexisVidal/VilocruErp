function CalculaIgv(valorBrut, porcIgv) {
    debugger;
    var valorNeto = parseFloat(valorBrut) / (1 + (parseFloat(porcIgv) / 100));
    var valorIgv = (valorBrut - valorNeto).toFixed(2);
    return valorIgv; 
}
function CalculaIgvMas(valorBrut, porcIgv) {
    debugger;
    var valorNeto = parseFloat(valorBrut) * ((parseFloat(porcIgv) / 100));
    var valorIgv = (valorNeto).toFixed(2);
    return valorIgv;
}