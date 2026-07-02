document.addEventListener('DOMContentLoaded', () => {
    carregarFormasPagamento();
});

async function carregarFormasPagamento() {
    try {
        const response = await fetch(`${API_BASE_URL}/FormasPagamento`, {
            method: 'GET',
            headers: getHeaders()
        });

        if (response.status === 401 || response.status === 403) {
            alert('Acesso negado ou sessão expirada. Por favor, faça login novamente.');
            localStorage.removeItem('token');
            window.location.href = '../../index.html';
            return;
        }

        if (response.ok) {
            const formas = await response.json();
            renderizarTabela(formas);
        }
    } catch (error) {
        console.error('Erro ao carregar dados:', error);
    }
}

function renderizarTabela(formas) {
    const tbody = document.querySelector('#tableFormasPagamento tbody');
    tbody.innerHTML = '';

    formas.forEach(forma => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${forma.id}</td>
            <td>${forma.nome}</td>
            <td>${forma.ativo ? '<span style="color: green;">Ativo</span>' : '<span style="color: red;">Inativo</span>'}</td>
            <td>
                <button onclick="editar(${forma.id})">Editar</button>
                <button onclick="excluir(${forma.id})">Excluir</button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}

function editar(id) {
    window.location.href = `form.html?id=${id}`;
}

async function excluir(id) {
    if (!confirm('Deseja realmente excluir esta forma de pagamento? O histórico pode ser afetado.')) return;

    try {
        const response = await fetch(`${API_BASE_URL}/FormasPagamento/${id}`, {
            method: 'DELETE',
            headers: getHeaders()
        });

        if (response.ok || response.status === 204) {
            alert('Excluído com sucesso!');
            carregarFormasPagamento();
        } else if (response.status === 403) {
            alert('Você não tem permissão. Apenas gerentes podem excluir.');
        } else {
            alert('Erro ao excluir. Verifique se a forma de pagamento já possui vínculo com algum pedido.');
        }
    } catch (error) {
        console.error('Erro ao excluir:', error);
    }
}