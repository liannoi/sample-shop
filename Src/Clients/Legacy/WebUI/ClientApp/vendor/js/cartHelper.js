'use strict';

class CartHelper {
    constructor(parentSelector, action, positiveMessage) {
        this.parentSelector = parentSelector;
        this.action = action;
        this.positiveMessage = positiveMessage;
    }

    subscribe() {
        const self = this;
        this.parentSelector.forEach(e => {
            e.addEventListener('click',
                function(event) {
                    event.preventDefault();
                    self.ask(this);
                });
        });
    }

    ask(item) {
        const self = this;
        return Swal.fire({
            title: 'Are you sure?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes'
        }).then(async (result) => {
            if (result.value) {
                await self.request(item.dataset.id);
            }
        });
    }

    async request(id) {
        const self = this;
        return await fetch(new Request(self.action),
            {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ goodId: id })
            }).then(
            function(response) {
                if (response.status === 200) {
                    success();
                }
            });

        function success() {
            return Swal.fire(
                'Added.',
                self.positiveMessage,
                'success'
            );
        }
    }
}