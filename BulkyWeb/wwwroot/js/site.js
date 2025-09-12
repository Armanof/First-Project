async function openEntityModal(controller, action, id = null, modalSize = 'max-w-lg') {
    const modalId = `modal-${controller.toLowerCase()}`;
    const url = `/${controller}/${action}${id ? `?id=${id}` : ''}`;

    try {
        const res = await fetch(url);
        const html = await res.text();

        const modal = document.createElement('div');
        modal.id = modalId;

        // modalSize is applied here, e.g., 'max-w-lg', 'max-w-2xl'
        modal.innerHTML = `
            <div id="modalBackdrop" class="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                <div class="bg-white p-6 rounded shadow-lg w-full relative overflow-y-auto max-h-[90vh] ${modalSize}">
                    <button id="modalCloseBtn" class="absolute top-3 right-3 text-gray-500 hover:text-gray-700 text-2xl font-bold" aria-label="Close modal">&times;</button>
                    <form id="entityForm" method="post" enctype="multipart/form-data" action="/${controller}/${action}">
                        <div id="modalFormBody">${html}</div>
                        <div class="mt-4 text-right">
                            <button type="submit" class="bg-blue-600 text-white px-4 py-2 rounded hover:bg-blue-700">
                                Save
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        `;

        document.body.appendChild(modal);

        // Image browse handlers
        document.addEventListener('click', function (e) {
            if (e.target && e.target.id === 'browseImageBtn') {
                document.getElementById('imageFileInput').click();
            }
        });

        document.addEventListener('change', function (e) {
            if (e.target && e.target.id === 'imageFileInput') {
                const fileInput = e.target;
                const displayInput = document.getElementById('imageUrlDisplay');
                if (fileInput.files.length > 0) {
                    displayInput.value = fileInput.files[0].name;
                }
            }
        });

        modal.querySelector('#modalCloseBtn').addEventListener('click', () => modal.remove());
        modal.querySelector('#modalBackdrop').addEventListener('click', (e) => {
            if (e.target.id === 'modalBackdrop') modal.remove();
        });

        const form = modal.querySelector('#entityForm');
        form.addEventListener('submit', async function (e) {
            e.preventDefault();
            const formData = new FormData(form);

            try {
                const response = await fetch(form.action, {
                    method: 'POST',
                    body: formData
                });

                if (response.ok) {
                    modal.remove();
                    Swal.fire({
                        icon: 'success',
                        title: 'Success',
                        text: 'Saved successfully!',
                        timer: 1500,
                        confirmButtonText: 'OK',
                        customClass: {
                            confirmButton: 'swal2-confirm bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700'
                        }
                    });
                    setTimeout(() => location.reload(), 1600);
                } else {
                    const error = await response.text();
                    const obj = JSON.parse(error);
                    const message = obj.errorMessage;

                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        html: message || 'Something went wrong!',
                        confirmButtonText: 'OK',
                        customClass: {
                            confirmButton: 'swal2-confirm bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700'
                        }
                    });
                }
            } catch (err) {
                modal.remove();
                Swal.fire({
                    icon: 'error',
                    title: 'Request Failed',
                    text: err.message || 'Unexpected error occurred.',
                    confirmButtonText: 'OK',
                    customClass: {
                        confirmButton: 'swal2-confirm bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700'
                    }
                });
            }
        });

    } catch (err) {
        Swal.fire({
            icon: 'error',
            title: 'Error loading form',
            text: err.message,
            confirmButtonText: 'OK',
            customClass: {
                confirmButton: 'swal2-confirm bg-red-600 text-white px-4 py-2 rounded hover:bg-red-700'
            }
        });
    }
}


function closeEntityModal(modalId) {
    document.getElementById(modalId)?.remove();
}

async function callApi(url, method = 'GET', data = null, headers = {}) {
    const result = await Swal.fire({
        title: 'Are you sure?',
        text: "Do you want to proceed with this action?",
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: 'Yes, proceed',
        cancelButtonText: 'Cancel',
        customClass: {
            confirmButton: 'swal2-confirm bg-green-600 text-white px-4 py-2 rounded hover:bg-green-700',
            cancelButton: 'swal2-cancel bg-gray-300 text-black px-4 py-2 rounded hover:bg-gray-400'
        }
    });

    if (!result.isConfirmed) {
        await Swal.fire({
            icon: 'info',
            title: 'Cancelled',
            text: 'Action was cancelled by the user.',
            timer: 1500,
            showConfirmButton: false
        });
        return null;
    }

    try {
        const response = await fetch(url, {
            method: method,
            headers: {
                'Content-Type': 'application/json',
                ...headers
            },
            body: data ? JSON.stringify(data) : null
        });

        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        setTimeout(() => {
            location.reload();
        }, 1600);

        const resultData = await response.json();
        return resultData;

    } catch (error) {
        console.error('API call failed:', error);
        await Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Something went wrong while processing your request.'
        });
        return null;
    }
}




