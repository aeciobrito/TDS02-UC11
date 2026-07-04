const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get("id");

async function carregarPedido() {

    try {

        const response = await fetch(
            `${API_BASE_URL}/Pedidos/${id}`,
            {
                headers: getHeaders()
            }
        );

        if (!response.ok) {
            throw new Error("Erro ao carregar pedido");
        }

        const pedido = await response.json();

        document.getElementById("dados-pedido").innerHTML = `
            <p><strong>ID:</strong> ${pedido.id}</p>
            <p><strong>Data:</strong> ${new Date(pedido.dataPedido).toLocaleDateString()}</p>
            <p><strong>Status:</strong> ${pedido.status}</p>
            <p><strong>Total:</strong> R$ ${pedido.total.toFixed(2)}</p>
        `;

    } catch (error) {

        console.error(error);

        document.getElementById("dados-pedido").innerHTML =
            "<p style='color:red'>Erro ao carregar pedido.</p>";
    }
}

document.getElementById("btn-cancelar")
.addEventListener("click", async () => {

    if (!confirm("Deseja realmente cancelar este pedido?")) {
        return;
    }

    try {

        const response = await fetch(
            `${API_BASE_URL}/Pedidos/${id}/cancelar`,
            {
                method: "PUT",
                headers: getHeaders()
            }
        );

        if (!response.ok) {
            throw new Error("Erro ao cancelar pedido");
        }

        alert("Pedido cancelado com sucesso!");

        window.location.href = "index.html";

    } catch (error) {

        console.error(error);

        alert("Erro ao cancelar pedido.");
    }
});

carregarPedido();