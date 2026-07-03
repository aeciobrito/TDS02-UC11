const urlParams = new URLSearchParams(window.location.search);
const id = urlParams.get('id');

const form = document.getElementById('form-conta');

async function carregarClientes() {
    try {
        const response = await fetchWithToken(`${API_BASE_URL}/api/Usuarios`);
        if (!response.ok) throw new Error('Erro ao carregar clientes');

        const usuarios = await response.json();
        const select = document.getElementById('clienteId');
        select.innerHTML = '<option value="">Selecione um cliente</option>';

        usuarios
            .filter(usuario => usuario.perfil === 'Cliente')
            .forEach(cliente => {
                const option = document.createElement('option');
                option.value = cliente.id;
                option.textContent = cliente.nome;
                select.appendChild(option);
            });
    } catch (error) {
        console.error('Erro ao carregar clientes:', error);
        document.getElementById('feedback-message').textContent = 'Erro ao carregar a lista de clientes. A criação/edição de contas não será possível.';
    }
}

async function carregarConta() {
    await carregarClientes();

    if (id) {
        document.getElementById('titulo-pagina').innerText = 'Editar Conta a Receber';

        try {
            const response = await fetchWithToken(`${API_BASE_URL}/api/ContasReceber/${id}`);
            if (!response.ok) throw new Error('Erro ao carregar conta');

            const conta = await response.json();

            document.getElementById('descricao').value = conta.descricao;
            document.getElementById('valor').value = conta.valor;
            document.getElementById('dataVencimento').value = formatarDataParaInput(conta.dataVencimento);
            document.getElementById('dataPagamento').value = conta.dataPagamento ? formatarDataParaInput(conta.dataPagamento) : '';
            document.getElementById('status').value = conta.status;
            document.getElementById('clienteId').value = conta.clienteId;
        } catch (error) {
            console.error('Erro ao carregar conta:', error);
            document.getElementById('feedback-message').textContent = 'Erro ao carregar os dados da conta. Tente recarregar a página.';
        }
    }
}

function formatarDataParaInput(dataISO) {
    const data = new Date(dataISO);
    const dia = String(data.getDate()).padStart(2, '0');
    const mes = String(data.getMonth() + 1).padStart(2, '0');
    const ano = data.getFullYear();
    return `${ano}-${mes}-${dia}`;
}

form.addEventListener('submit', async (e) => {
    e.preventDefault();

    const descricao = document.getElementById('descricao').value.trim();
    const valor = parseFloat(document.getElementById('valor').value);
    const dataVencimento = document.getElementById('dataVencimento').value;
    const dataPagamento = document.getElementById('dataPagamento').value;
    const status = document.getElementById('status').value;
    const clienteId = parseInt(document.getElementById('clienteId').value, 10);

    if (!descricao || Number.isNaN(valor) || valor < 0 || !dataVencimento || !status || Number.isNaN(clienteId) || clienteId <= 0) {
        alert('Por favor, preencha todos os campos obrigatórios corretamente');
        return;
    }

    if (dataPagamento && dataVencimento > dataPagamento) {
        alert('A data de pagamento não pode ser anterior à data de vencimento');
        return;
    }

    const contaDados = {
        id: id ? parseInt(id, 10) : 0,
        descricao: descricao,
        valor: valor,
        dataVencimento: new Date(dataVencimento + 'T00:00:00').toISOString(),
        dataPagamento: dataPagamento ? new Date(dataPagamento + 'T00:00:00').toISOString() : null,
        status: status,
        clienteId: clienteId
    };

    const method = id ? 'PUT' : 'POST';
    const url = id ? `${API_BASE_URL}/api/ContasReceber/${id}` : `${API_BASE_URL}/api/ContasReceber`;

    try {
        const response = await fetchWithToken(url, {
            method: method,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(contaDados)
        });

        if (!response.ok) throw new Error('Erro ao salvar conta');

        window.location.href = 'index.html';
    } catch (error) {
        console.error('Erro ao salvar:', error);
        alert('Erro ao salvar a conta a receber. Tente novamente.');
    }
});

carregarConta();
