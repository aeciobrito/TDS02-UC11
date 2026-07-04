async function carregarPedidos() {

    try {

        const response = await fetch(
            `${API_BASE_URL}/Pedidos`,
            {
                method: 'GET',
                headers: getHeaders()
            }
        );

        const pedidos = await response.json();

        const tbody = document.getElementById('tabela-pedidos');

        tbody.innerHTML = '';

        pedidos.forEach(pedido => {

            const tr = document.createElement('tr');

            tr.innerHTML = `
                <td>${pedido.id}</td>
                <td>${new Date(pedido.dataPedido).toLocaleDateString()}</td>
                <td>${obterStatus(pedido.status)}</td>
                <td>R$ ${pedido.total.toFixed(2)}</td>

                <td class="actions">
                    <a href="detalhes.html?id=${pedido.id}">
                        Detalhes
                    </a>
                      <a href="form.html?id=${pedido.id}">
                        Alterar Status
                    </a>

                    <a href="cancelar.html?id=${pedido.id}" style="color: var(--danger-color);">
                        Cancelar
                    </a>
                </td>
            `;

            tbody.appendChild(tr);
        });

    } catch(error) {

        console.error(error);
    }
}

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

carregarPedidos();