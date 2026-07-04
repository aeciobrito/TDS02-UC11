const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get('id');

const form = document.getElementById('form-pedido');

async function carregarPedido() {

    if (!id) {
        alert("Pedido não informado.");
        window.location.href = "index.html";
        return;
    }

    try {

        const response = await fetch(
            `${API_BASE_URL}/Pedidos/${id}`,
            {
                headers: getHeaders()
            }
        );

        if (!response.ok)
            throw new Error("Erro ao carregar pedido");

        const pedido = await response.json();

        document.getElementById('dados-pedido').innerHTML = `
            <p><strong>ID:</strong> ${pedido.id}</p>
            <p><strong>Data:</strong> ${new Date(pedido.dataPedido).toLocaleDateString()}</p>
            <p><strong>Status Atual:</strong> ${pedido.status}</p>
            <p><strong>Total:</strong> R$ ${pedido.total.toFixed(2)}</p>
        `;

        const mapaStatus = {
            "Aberto": 1,
            "Fechado": 2,
            "Suspenso": 3,
            "Cancelado": 4
        };

        document.getElementById('status').value =
            mapaStatus[pedido.status];

    } catch (error) {

        console.error(error);
        alert("Erro ao carregar pedido.");
    }
}

form.addEventListener('submit', async (e) => {

    e.preventDefault();

    const status = parseInt(
        document.getElementById('status').value
    );

    try {

        const response = await fetch(
            `${API_BASE_URL}/Pedidos/${id}/status`,
            {
                method: 'PUT',
                headers: {
                    ...getHeaders(),
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    status: status
                })
            }
        );

        if (!response.ok)
            throw new Error();

        alert("Status atualizado com sucesso!");

        window.location.href = "index.html";

    } catch (error) {

        console.error(error);
        alert("Erro ao atualizar status.");
    }
});

carregarPedido();