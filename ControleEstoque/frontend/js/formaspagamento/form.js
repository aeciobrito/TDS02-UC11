const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get('id');

const form = document.getElementById('form-formapagamento');

async function carregarFormaPagamento() {
    if (id) {
        document.getElementById('titulo-pagina').innerText =
            "Editar Forma de Pagamento";

        try {
            const response = await fetch(
                `${API_BASE_URL}/FormasPagamento/${id}`
            );

            if (!response.ok)
                throw new Error('Erro ao carregar forma de pagamento');

            const formaPagamento = await response.json();

            document.getElementById('nome').value =
                formaPagamento.nome;

        } catch (error) {
            console.error(error);
            alert('Erro ao carregar os dados');
        }
    }
}

form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const nome = document.getElementById('nome').value.trim();

    if (!nome) {
        alert('Informe o nome da forma de pagamento');
        return;
    }

    const formaPagamento = {
        id: id ? parseInt(id) : 0,
        nome: nome
    };

    const method = id ? 'PUT' : 'POST';

    const url = id
        ? `${API_BASE_URL}/FormasPagamento/${id}`
        : `${API_BASE_URL}/FormasPagamento`;

    try {
        const response = await fetch(url, {
            method: method,
            headers: getHeaders(),
            body: JSON.stringify(formaPagamento)
        });

        if (!response.ok)
            throw new Error('Erro ao salvar');

        window.location.href = 'index.html';

    } catch (error) {
        console.error(error);
        alert('Erro ao salvar a forma de pagamento');
    }
});

carregarFormaPagamento();