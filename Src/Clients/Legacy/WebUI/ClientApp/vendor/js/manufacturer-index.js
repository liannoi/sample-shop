'use strict';

function test() {
    const softDeleter = new SoftDeleter(document.querySelectorAll('.btn-cmd-delete'),
        '/Manufacturer/Delete',
        'Manufacturer successfully removed.');
    softDeleter.subscribeToDelete();
}

test();