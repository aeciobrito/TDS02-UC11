const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get("id");

function obterStatus(status) {

    switch (status) {
        case 1:
            return "Aberto";

        case 2:
            return "Fechado";

        case 3:
            return "Suspenso";

        case 4:
            return "Cancelado";

        default:
            return "Desconhecido";
    }
}

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
            <p><strong>Status:</strong> ${obterStatus(pedido.status)}</p>
            <p><strong>Cliente:</strong> ${pedido.clienteId}</p>
            <p><strong>Total:</strong> R$ ${pedido.total.toFixed(2)}</p>
        `;

        const tbody =
            document.getElementById("tabela-itens");

        tbody.innerHTML = "";

        pedido.itens.forEach(item => {

            const subtotal =
                item.quantidade * item.precoUnitario;

            tbody.innerHTML += `
                <tr>
                    <td>${item.produtoNome}</td>
                    <td>${item.quantidade}</td>
                    <td>R$ ${item.precoUnitario.toFixed(2)}</td>
                    <td>R$ ${subtotal.toFixed(2)}</td>
                </tr>
            `;
        });

    }
    catch(error) {

        console.error(error);

        document.getElementById(
            "dados-pedido"
        ).innerHTML = `
            <p style="color:red">
                Erro ao carregar pedido.
            </p>
        `;
    }
}

carregarPedido();