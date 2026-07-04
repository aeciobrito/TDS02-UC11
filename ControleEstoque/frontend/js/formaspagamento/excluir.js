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
            throw new Error('Erro ao carregar forma de pagamento');

        const formaPagamento = await response.json();

        document.getElementById('dados-formapagamento').innerHTML = `
            <h3>${formaPagamento.nome}</h3>
        `;

    } catch (error) {
        console.error("Erro ao carregar detalhes:", error);

        document.getElementById('dados-formapagamento').innerHTML =
            `<p style="color:red;">Erro ao carregar os dados.</p>`;
    }
}

document.getElementById('btn-excluir').addEventListener('click', async () => {
    try {

        const response = await fetch(
            `${API_BASE_URL}/FormasPagamento/${id}`,
            {
                method: 'DELETE',
                headers: getHeaders()
            }
        );

        if (!response.ok)
            throw new Error('Erro ao excluir');

        window.location.href = 'index.html';

    } catch (error) {
        console.error("Erro ao excluir:", error);
        alert('Erro ao excluir a forma de pagamento');
    }
});

buscarDetalhes();