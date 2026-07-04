async function carregarFormasPagamento() {
    try {
        const response = await fetch(
            `${API_BASE_URL}/FormasPagamento`,
            {
                method: 'GET',
                headers: getHeaders()
            }
        );

        if (response.status == 401) {
            alert("Sessão expirada! Por favor, faça login novamente!");
            localStorage.removeItem('token');
            window.location.href = '../../index.html';
            return;

        } else if (response.status == 403) {
            alert("Sem acesso ao recurso!");
            window.location.href = '../../index.html';
            return;
        }

        const formasPagamento = await response.json();

        const tbody = document.getElementById('tabela-formapagamentos');
        tbody.innerHTML = '';

        formasPagamento.forEach(forma => {
            const tr = document.createElement('tr');

            tr.innerHTML = `
                <td>${forma.id}</td>
                <td>${forma.nome}</td>
                <td class="actions">
                    <a href="detalhes.html?id=${forma.id}">Detalhes</a>
                    <a href="form.html?id=${forma.id}">Editar</a>
                    <a href="excluir.html?id=${forma.id}" style="color: var(--danger-color);">Excluir</a>
                </td>
            `;

            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("Erro ao carregar as formas de pagamento:", error);
    }
}

carregarFormasPagamento();