const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get('id');

async function buscarDetalhes() {
    try {

        const response = await fetch(
            `${API_BASE_URL}/FormasPagamento/${id}`,
            {
                headers: getHeaders()
            }
        );

        if (!response.ok)
            throw new Error('Erro ao carregar detalhes');

        const formaPagamento = await response.json();

        document.getElementById('dados-formapagamento').innerHTML = `
            <p><strong>ID:</strong> ${formaPagamento.id}</p>
            <p><strong>Nome:</strong> ${formaPagamento.nome}</p>
        `;

    } catch (error) {

        document.getElementById('dados-formapagamento').innerHTML =
            `<p style="color:red;">Erro ao carregar detalhes.</p>`;

        console.error(error);
    }
}

buscarDetalhes();