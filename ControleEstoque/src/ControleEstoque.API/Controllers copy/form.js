const urlParams = new URLSearchParams(window.location.search);
const idEdicao = urlParams.get('id');

document.addEventListener('DOMContentLoaded', () => {
    if (idEdicao) {
        carregarDadosEdicao(idEdicao);
    }
});

async function carregarDadosEdicao(id) {
    try {
        const response = await fetch(`${API_BASE_URL}/FormasPagamento/${id}`, {
            method: 'GET',
            headers: getHeaders()
        });

        if (response.ok) {
            const forma = await response.json();
            document.getElementById('id').value = forma.id;
            document.getElementById('nome').value = forma.nome;
            document.getElementById('ativo').value = forma.ativo.toString();
        } else {
            alert('Erro ao carregar dados para edição.');
        }
    } catch (error) {
        console.error('Erro:', error);
    }
}

document.getElementById('formaPagamentoForm').addEventListener('submit', async (e) => {
    e.preventDefault();

    const id = document.getElementById('id').value;
    const nome = document.getElementById('nome').value;
    const ativo = document.getElementById('ativo').value === 'true';
    
    const method = id ? 'PUT' : 'POST';
    const url = id ? `${API_BASE_URL}/FormasPagamento/${id}` : `${API_BASE_URL}/FormasPagamento`;
    const payload = { nome, ativo };

    try {
        const response = await fetch(url, {
            method: method,
            headers: getHeaders(),
            body: JSON.stringify(payload)
        });

        if (response.ok || response.status === 201) {
            alert('Salvo com sucesso!');
            window.location.href = 'index.html';
        } else if (response.status === 403) {
            alert('Você não tem permissão para esta ação (Apenas Gerentes).');
        } else {
            const errorData = await response.json().catch(() => ({}));
            alert(`Erro ao salvar os dados: ${errorData.message || 'Erro desconhecido'}`);
        }
    } catch (error) {
        console.error('Erro de requisição:', error);
    }
});