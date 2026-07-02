async function carregarFornecedores() {
    try {
        const response = await fetchWithToken(`${API_BASE_URL}/api/Fornecedores`);
        const fornecedores = await response.json();
        
        const tbody = document.getElementById('tabela-fornecedores');
        tbody.innerHTML = '';

        fornecedores.forEach(fornecedor => {
            const tr = document.createElement('tr');
            tr.innerHTML = `
                <td>${fornecedor.id}</td>
                <td>${fornecedor.nomeFantasia}</td>
                <td>${fornecedor.cnpj}</td>
                <td class="actions">
                    <a href="detalhes.html?id=${fornecedor.id}">Detalhes</a>
                    <a href="form.html?id=${fornecedor.id}">Editar</a>
                    <a href="excluir.html?id=${fornecedor.id}" style="color: var(--danger-color);">Excluir</a>
                </td>
            `;
            tbody.appendChild(tr);
        });
    } catch (error) {
        console.error("Erro ao carregar os fornecedores:", error);
    }
}

carregarFornecedores();