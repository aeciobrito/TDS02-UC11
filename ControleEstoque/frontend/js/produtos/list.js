async function carregarProdutos() {
    const feedback = document.getElementById('feedback-message');
    try {
        const response = await fetchWithToken(`${API_BASE_URL}/api/Produtos`);
        if (!response.ok) {
            throw new Error('Falha ao carregar os produtos. Status: ' + response.status);
        }
        const produtos = await response.json();
        
        if (produtos.length === 0) {
            feedback.textContent = 'Nenhum produto encontrado.';
            feedback.style.display = 'block';
        }

        const tbody = document.getElementById('tabela-produtos');
        tbody.innerHTML = '';

        produtos.forEach(produto => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${produto.id}</td>
                <td>${produto.nome}</td>
                <td>R$ ${produto.preco.toFixed(2)}</td>
                <td>${produto.fornecedor?.nomeFantasia || 'Não informado'}</td>
                <td class="actions">
                    <a href="detalhes.html?id=${produto.id}">Detalhes</a>
                    <a href="form.html?id=${produto.id}">Editar</a>
                    <a href="excluir.html?id=${produto.id}" style="color: var(--danger-color);">Excluir</a>
                </td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error("Erro ao carregar os produtos:", error);
        feedback.textContent = 'Erro ao carregar a lista de produtos. Tente novamente mais tarde.';
        feedback.style.display = 'block';
    }
}

carregarProdutos();